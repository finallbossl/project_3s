using System.Collections.Generic;
using System.Threading.Tasks;
using FSA_3S.Models.Entities;

public interface IMappingUserAppointmentRepository
{
    Task<IEnumerable<MappingUserAppointmentEntity>> GetAllAsync();
    Task<MappingUserAppointmentEntity> GetByIdAsync(int id);
    Task<MappingUserAppointmentEntity> AddAsync(MappingUserAppointmentEntity mapping);
    Task<MappingUserAppointmentEntity> UpdateAsync(MappingUserAppointmentEntity mapping);
    Task DeleteAsync(int id);
}