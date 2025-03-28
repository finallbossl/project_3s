using System.Collections.Generic;
using System.Threading.Tasks;
using FSA_3S.Models.Entities;

public interface IAppointmentRepository
{
    IQueryable<AppointmentEntity> Appointments { get; }

    Task<IEnumerable<AppointmentEntity>> GetAllAsync();
    Task<AppointmentEntity> GetByIdAsync(int id);
    Task<AppointmentEntity> AddAsync(AppointmentEntity appointment);
    Task<AppointmentEntity> UpdateAsync(AppointmentEntity appointment);
    Task DeleteAsync(int id);
}