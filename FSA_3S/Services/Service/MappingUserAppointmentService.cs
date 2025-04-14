using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks;
using FSA_3S.Helpers;
using FSA_3S.Models.Entities;
using FSA_3S.Helpers;
using FSA_3S.Enum;
using FSA_3S.Models.Requests;
using System.Xml;
using FSA_3S.Services.Interface;
using Microsoft.AspNetCore.SignalR;
using FSA_3S.Models;
using FSA_3S.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using FSA_3S.Models.Respone;
namespace FSA_3S.Services.Service
{


    public class MappingUserAppointmentService(IMappingUserAppointmentRepository _repository, IHttpContextAccessor _httpContextAccessor, IAppointmentService _appointmentService, INotificationService _notificationService, IHubContext<NotificationHub> _hubContext, IStaffRepository _staffRepository, IUserRepository _userRepository, AppDbContext _context) : IMappingUserAppointmentService
    {
        public async Task<IEnumerable<MappingUserAppointmentResponse>> GetAllMappingsAsync()
        {
            var mappings = await _repository.GetAllAsync();

            var response = mappings.Where(m => m.Appointment != null && m.ApprovalStatus == ApprovalStatusEnum.Approved)
                    .Select(m => new MappingUserAppointmentResponse
                    {
                        MappingUserAppointmentId = m.MappingUserAppointmentId,
                        UserId = m.UserId,
                        FullName = m.User != null ? m.User.FullName : "N/A", 
                        AppointmentId = m.AppointmentId,
                        Status = m.Status,
                        AppointmentTitle = m.Appointment != null ? m.Appointment.Title : "N/A",
                        AppointmentDate = m.Appointment.AppointmentDate,
                        CustomerName = m.Appointment != null && m.Appointment.Customer != null
                    ? m.Appointment.Customer.FullName
                    : "N/A"
                    });

            return response.ToList();
        }

        public async Task<MappingUserAppointmentEntity> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<MappingUserAppointmentResponse> CreateAsync(MappingUserAppointmentRequest request)

        {
            int createBy = UserIdHelper.GetUserId(_httpContextAccessor)
                ?? throw new UnauthorizedAccessException("Invalid or missing user ID.");

            var newAppointment = new AppointmentEntity
            {
                CustomerId = request.CustomerId,
                Title = request.Title ?? "Default Title",
                Description = request.Description,
                AppointmentDate = request.AppointmentDate ?? DateOnly.FromDateTime(DateTime.UtcNow),
               
                Status = request.Status ?? "Pending",
                Address = request.Address,
                CreatedBy = createBy,
                UpdatedBy = null,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,


            };
            var createdAppointment = await _appointmentService.CreateAsync(newAppointment);
            var mapping = new MappingUserAppointmentEntity
            {
                UserId = request.UserId,
                AppointmentId = createdAppointment.AppointmentId,
                ApprovalStatus = ApprovalStatusEnum.NotApproved,
             
                Status = request.Status ?? "notPending",
                CreatedBy = createBy,
                UpdatedBy = null,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            try
            {
                var result = await _repository.AddAsync(mapping);
                var createdByUser = await _staffRepository.GetUserByIdAsync(createBy) ?? throw new ArgumentException("Người dùng không tồn tại.");
                string createdByFullName = createdByUser?.FullName ?? "Người dùng không xác định";
                string userRole = createdByUser?.Role ?? "Unknown";

                if (userRole != "admin")
                {
                    var adminId = await _userRepository.GetAdminIdAsync();

                    var message = $"Người dùng {createdByFullName} đã tạo 1 lịch hẹn mới có tiêu đề {request.Title} cần duyệt.";

                    await _notificationService.NotificationPostRealAsync(
                        senderId: createBy,
                        receiverId: adminId,
                        title: "Lịch hẹn mới cần duyệt",
                        message: message
                    );

                    if (_hubContext != null)
                    {
                        await _hubContext.Clients.User(adminId.ToString()).SendAsync("ReceiveNotification", "Lịch hẹn mới cần duyệt.", message);
                    }
                }
                return new MappingUserAppointmentResponse
                {
                    MappingUserAppointmentId = result.MappingUserAppointmentId,
                    UserId = result.UserId,
                    AppointmentId = result.AppointmentId,
                    ApprovalStatus = result.ApprovalStatus,
                    Status = result.Status,
                    CreatedBy = result.CreatedBy,
                    UpdatedBy = result.UpdatedBy,
                    CreatedAt = result.CreatedAt,
                    UpdatedAt = result.UpdatedAt

                };
            }
            catch (Exception ex)
            {
                throw new Exception($"[CreateAsync] Failed to create real estate: {ex.InnerException?.Message ?? ex.Message}", ex);
            }

        }
        public async Task<MappingUserAppointmentResponse> UpdateStatusAsync(int id, string newStatus)
        {
            if (string.IsNullOrEmpty(newStatus) || id <= 0)
            {
                throw new ArgumentException("Invalid status or ID.");
            }

            var mappingUserAppointment = await _repository.GetByMappingIdAsync(id);
            if (mappingUserAppointment == null)
            {
                throw new KeyNotFoundException("MappingUserAppointment not found.");
            }

            mappingUserAppointment.Status = newStatus;

            mappingUserAppointment.UpdatedAt = DateTime.UtcNow;
            mappingUserAppointment.UpdatedBy = UserIdHelper.GetUserId(_httpContextAccessor); 

            await _repository.UpdateAsync(mappingUserAppointment);

            var response = new MappingUserAppointmentResponse
            {
                MappingUserAppointmentId = mappingUserAppointment.MappingUserAppointmentId,
                AppointmentId = mappingUserAppointment.AppointmentId,
                UserId = mappingUserAppointment.UserId,
                Status = mappingUserAppointment.Status,
                UpdatedAt = mappingUserAppointment.UpdatedAt,
                UpdatedBy = mappingUserAppointment.UpdatedBy
            };

            return response;
        }


        public async Task<bool> DeleteAsync(int id)
        {
     
     
            return await _repository.DeleteAsync(id);

     
        }

        public async Task<MappingUserAppointmentResponse?> UpdateApprovalStatusAsync(int id, ApprovalStatusRequest request)
        {
            var map = await _repository.GetByIdForPutAsync(id);
            if (map == null) return null;
            var AppointmentTitle = await _context.Appointments
               .Where(a => a.AppointmentId == map.AppointmentId)
               .Select(a => a.Title)
               .FirstOrDefaultAsync();
            var oldStatus = map.ApprovalStatus;

            map.ApprovalStatus = (ApprovalStatusEnum)request.ApprovalStatus;
            map.UpdatedAt = DateTime.UtcNow;

            var response = new MappingUserAppointmentResponse
            {
                ApprovalStatus = map.ApprovalStatus,
                MappingUserAppointmentId = map.MappingUserAppointmentId,
                UserId = map.UserId,
                AppointmentId = map.AppointmentId,
                CreatedBy = map.CreatedBy,
                UpdatedBy = map.UpdatedBy,
                CreatedAt = map.CreatedAt,
                UpdatedAt = map.UpdatedAt
            };

            var adminId = await _userRepository.GetAdminIdAsync();

            if (oldStatus == ApprovalStatusEnum.NotApproved &&
                (map.ApprovalStatus == ApprovalStatusEnum.Approved || map.ApprovalStatus == ApprovalStatusEnum.NotAllowed))
            {
                var message = $"Admin đã {map.ApprovalStatus.ToString().ToLower()} cuộc hẹn: {AppointmentTitle}.";

                await _notificationService.NotificationPutRealAsync(
                    senderId: adminId,
                    receiverId: map.CreatedBy,
                    title: "Trạng thái phê duyệt cuộc hẹn",
                    message: message
                );

                await _hubContext.Clients.User(map.CreatedBy.ToString())
                    .SendAsync("ReceiveNotification", "Trạng thái phê duyệt", message);
            }
            await _repository.UpdateApprovalStatusAsync(map);
            return response;
        }

        public async Task<IEnumerable<MappingUserAppointmentResponse>> GetAllMappingsApprovalStatusAsync()
        {
            var mappings = await _repository.GetAllAsync();

            var response = mappings.Where(m => m.Appointment != null && m.ApprovalStatus == ApprovalStatusEnum.NotApproved)
                    .Select(m => new MappingUserAppointmentResponse
                    {
                        MappingUserAppointmentId = m.MappingUserAppointmentId,
                        UserId = m.UserId,
                        FullName = m.User != null ? m.User.FullName : "N/A", 
                        AppointmentId = m.AppointmentId,
                        AppointmentTitle = m.Appointment != null ? m.Appointment.Title : "N/A", 
                        AppointmentDate = m.Appointment.AppointmentDate,
                        CustomerName = m.Appointment != null && m.Appointment.Customer != null
                    ? m.Appointment.Customer.FullName
                    : "N/A"
                    });

            return response.ToList();
        }
    }
}