using FSA_3S.Helpers;
using FSA_3S.Models;
using FSA_3S.Models.Entities;
using FSA_3S.Models.Requests;
using FSA_3S.Models.Respone;
using FSA_3S.Repositories.Interface;
using FSA_3S.Services.Interface;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FSA_3S.Services.Service
{
    public class WorkService(
        IWorkRepository workRepository,
        IStaffRepository staffRepository,
        IUserRepository userRepository,
        INotificationService notificationService,
        IHubContext<NotificationHub> hubContext, AppDbContext context
    ) : IWorkService
    {
        private readonly IWorkRepository _workRepository = workRepository ?? throw new ArgumentNullException(nameof(workRepository));
        private readonly IStaffRepository _staffRepository = staffRepository ?? throw new ArgumentNullException(nameof(staffRepository));
        private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        private readonly INotificationService _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
        private readonly IHubContext<NotificationHub> _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<WorkEntity?> CreateWorkAsync(WorkRequest request)
        {
            var user = await _staffRepository.GetUserByIdAsync(request.UserId)
                ?? throw new ArgumentException("User không tồn tại.");

            if (user.Role != "Staff")
                throw new UnauthorizedAccessException("Chỉ có thể tạo Work cho nhân viên (Staff).");

            var work = new WorkEntity
            {
                UserId = request.UserId,
                Monday = request.Monday,
                MondayTime = request.MondayTime,
                Tuesday = request.Tuesday,
                TuesdayTime = request.TuesdayTime,
                Wednesday = request.Wednesday,
                WednesdayTime = request.WednesdayTime,
                Thursday = request.Thursday,
                ThursdayTime = request.ThursdayTime,
                Friday = request.Friday,
                FridayTime = request.FridayTime,
                Saturday = request.Saturday,
                SaturdayTime = request.SaturdayTime,
                Sunday = request.Sunday,
                SundayTime = request.SundayTime
            };

            var adminId = await _userRepository.GetAdminIdAsync();
            var message = $"Admin đã tạo một công việc mới cho {user.FullName}.";

            await _notificationService.NotificationPostRealAsync(
                senderId: adminId,
                receiverId: request.UserId,
                title: "Công việc mới",
                message: message
            );

            if (_hubContext != null)
            {
                await _hubContext.Clients.User(request.UserId.ToString()).SendAsync("ReceiveNotification", "Công việc mới", message);
            }
            else
            {
                Console.WriteLine("SignalR _hubContext is null. Notification not sent.");
            }

            await _workRepository.AddWorkAsync(work);
            return work;
        }

        public async Task<IEnumerable<WorkResponse>> GetAllWorksAsync()
        {
            var works = await (from w in _context.WorkEntity
                               join u in _context.Users on w.UserId equals u.UserId
                               select new WorkResponse
                               {
                                   WorkId = w.WorkId,
                                   UserId = w.UserId,
                                   FullName = u.FullName, // Lấy từ UserEntity
                                   Monday = w.Monday,
                                   MondayTime = w.MondayTime,
                                   Tuesday = w.Tuesday,
                                   TuesdayTime = w.TuesdayTime,
                                   Wednesday = w.Wednesday,
                                   WednesdayTime = w.WednesdayTime,
                                   Thursday = w.Thursday,
                                   ThursdayTime = w.ThursdayTime,
                                   Friday = w.Friday,
                                   FridayTime = w.FridayTime,
                                   Saturday = w.Saturday,
                                   SaturdayTime = w.SaturdayTime,
                                   Sunday = w.Sunday,
                                   SundayTime = w.SundayTime
                               }).ToListAsync();

            return works;
        }

        public async Task<WorkEntity?> UpdateWorkAsync(int workId, WorkRequest request)
        {
            var work = await _workRepository.GetWorkByIdAsync(workId);
            if (work == null) return null;

            var user = await _staffRepository.GetUserByIdAsync(request.UserId);
            if (user == null || user.Role != "Staff")
            {
                throw new UnauthorizedAccessException("Chỉ có thể cập nhật Work cho nhân viên (Staff).");
            }

            work.Monday = request.Monday;
            work.MondayTime = request.MondayTime;
            work.Tuesday = request.Tuesday;
            work.TuesdayTime = request.TuesdayTime;
            work.Wednesday = request.Wednesday;
            work.WednesdayTime = request.WednesdayTime;
            work.Thursday = request.Thursday;
            work.ThursdayTime = request.ThursdayTime;
            work.Friday = request.Friday;
            work.FridayTime = request.FridayTime;
            work.Saturday = request.Saturday;
            work.SaturdayTime = request.SaturdayTime;
            work.Sunday = request.Sunday;
            work.SundayTime = request.SundayTime;

            var adminId = await _userRepository.GetAdminIdAsync();
            var message = $"Admin đã điều chỉnh lại công việc cho {user.FullName}.";

            await _notificationService.NotificationPostRealAsync(
                senderId: adminId,
                receiverId: request.UserId,
                title: "Chỉnh sửa công việc.",
                message: message
            );

            if (_hubContext != null)
            {
                await _hubContext.Clients.User(request.UserId.ToString()).SendAsync("ReceiveNotification", "Chỉnh sửa công việc.", message);
            }
            else
            {
                Console.WriteLine("SignalR _hubContext is null. Notification not sent.");
            }

            await _workRepository.UpdateWorkAsync(work);
            return work;
        }

        public async Task<bool> DeleteWorkAsync(int workId)
        {
            var work = await _workRepository.GetWorkByIdAsync(workId);
            if (work == null) return false;

            var user = await _staffRepository.GetUserByIdAsync(work.UserId);
            if (user == null) return false;

            var adminId = await _userRepository.GetAdminIdAsync();
            var message = $"Admin đã xóa công việc của {user.FullName}.";

            await _notificationService.NotificationPostRealAsync(
                senderId: adminId,
                receiverId: work.UserId,
                title: "Xóa công việc",
                message: message
            );

            if (_hubContext != null)
            {
                await _hubContext.Clients.User(work.UserId.ToString()).SendAsync("ReceiveNotification", "Hủy công việc", message);
            }
            else
            {
                Console.WriteLine("SignalR _hubContext is null. Notification not sent.");
            }

            await _workRepository.DeleteWorkAsync(work);
            return true;
        }
    }
}