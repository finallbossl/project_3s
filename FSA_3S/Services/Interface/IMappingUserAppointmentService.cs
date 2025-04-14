using System.Collections.Generic;
using System.Threading.Tasks;
using FSA_3S.Models.Entities;
using FSA_3S.Models.Requests;
using FSA_3S.Models.Respone;

public interface IMappingUserAppointmentService
{
    Task<IEnumerable<MappingUserAppointmentResponse>> GetAllMappingsAsync();
    Task<IEnumerable<MappingUserAppointmentResponse>> GetAllMappingsApprovalStatusAsync();
    Task<MappingUserAppointmentEntity> GetByIdAsync(int id);
    Task<MappingUserAppointmentResponse?> CreateAsync(MappingUserAppointmentRequest request);
    Task<MappingUserAppointmentResponse?> UpdateStatusAsync(int id, string newStatus);
    Task<MappingUserAppointmentResponse?> UpdateApprovalStatusAsync(int id, ApprovalStatusRequest request);
    Task<bool> DeleteAsync(int id) ;
   
}