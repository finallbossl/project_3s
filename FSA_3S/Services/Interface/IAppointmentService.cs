using System.Collections.Generic;
using System.Threading.Tasks;
using FSA_3S.Models.Entities;

public interface IAppointmentService
{
    Task<IEnumerable<AppointmentEntity>> GetAllAsync();
    Task<AppointmentEntity> GetByIdAsync(int id);
    Task<AppointmentEntity> CreateAsync(AppointmentEntity appointment);
    Task<AppointmentEntity> UpdateAsync(AppointmentEntity appointment);
    Task DeleteAsync(int id);
   
}