using System.Collections.Generic;
using System.Threading.Tasks;
using FSA_3S.Models.Entities;
using Microsoft.EntityFrameworkCore;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _repository;

    public AppointmentService(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AppointmentEntity>> GetAllAsync()
    {
        var appointments = await _repository.Appointments 
        .Include(a => a.Customer)
        .ToListAsync();

        return appointments;
    }

    public async Task<AppointmentEntity> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<AppointmentEntity> CreateAsync(AppointmentEntity appointment)
    {
        return await _repository.AddAsync(appointment);
    }

    public async Task<AppointmentEntity> UpdateAsync(AppointmentEntity appointment)
    {
        return await _repository.UpdateAsync(appointment);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}