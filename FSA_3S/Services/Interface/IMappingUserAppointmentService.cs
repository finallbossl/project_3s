using System.Collections.Generic;
using System.Threading.Tasks;
using FSA_3S.Models.Entities;

public interface IMappingUserAppointmentService
{
    Task<IEnumerable<MappingUserAppointmentResponse>> GetAllMappingsAsync();
    Task<MappingUserAppointmentEntity> GetByIdAsync(int id);
    Task<MappingUserAppointmentEntity> CreateAsync(MappingUserAppointmentEntity mapping);
    Task<MappingUserAppointmentEntity> UpdateAsync(MappingUserAppointmentEntity mapping);
    Task DeleteAsync(int id);
}