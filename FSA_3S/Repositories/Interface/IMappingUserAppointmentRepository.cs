using System.Collections.Generic;
using System.Threading.Tasks;
using FSA_3S.Models.Entities;

public interface IMappingUserAppointmentRepository
{
    Task<IEnumerable<MappingUserAppointmentEntity>> GetAllAsync();
    Task<IEnumerable<MappingUserAppointmentEntity>> GetAllApprovalStatusAsync();
    Task<MappingUserAppointmentEntity> GetByIdAsync(int id);
    Task<MappingUserAppointmentEntity> GetByMappingIdAsync(int id);
    Task<MappingUserAppointmentEntity> GetByIdForPutAsync(int id);
    Task<MappingUserAppointmentEntity> AddAsync(MappingUserAppointmentEntity mapping);
    Task<MappingUserAppointmentEntity> UpdateAsync(MappingUserAppointmentEntity mapping);
    Task<MappingUserAppointmentEntity> UpdateApprovalStatusAsync(MappingUserAppointmentEntity mapping);
    Task<bool> DeleteAsync(int id);
    
}