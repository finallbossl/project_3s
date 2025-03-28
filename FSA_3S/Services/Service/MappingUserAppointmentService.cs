using System.Collections.Generic;
using System.Linq; // Đảm bảo đã thêm using này
using System.Threading.Tasks;
using FSA_3S.Models.Entities;

public class MappingUserAppointmentService : IMappingUserAppointmentService
{
    private readonly IMappingUserAppointmentRepository _repository;

    public MappingUserAppointmentService(IMappingUserAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<MappingUserAppointmentResponse>> GetAllMappingsAsync()
    {
        var mappings = await _repository.GetAllAsync();

        var response = mappings.Select(m => new MappingUserAppointmentResponse
        {
            MappingUserAppointmentId = m.MappingUserAppointmentId,
            UserId = m.UserId,
            FullName = m.User != null ? m.User.FullName : "N/A", // Kiểm tra null
            AppointmentId = m.AppointmentId,
            AppointmentTitle = m.Appointment != null ? m.Appointment.Title : "N/A", // Kiểm tra null
            CustomerName = m.Appointment != null && m.Appointment.Customer != null
                ? m.Appointment.Customer.FullName
                : "N/A" // Kiểm tra null
        });

        return response.ToList();
    }

    public async Task<MappingUserAppointmentEntity> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<MappingUserAppointmentEntity> CreateAsync(MappingUserAppointmentEntity mapping)
    {
        return await _repository.AddAsync(mapping);
    }

    public async Task<MappingUserAppointmentEntity> UpdateAsync(MappingUserAppointmentEntity mapping)
    {
        return await _repository.UpdateAsync(mapping);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}