using FSA_3S.DTOs;

namespace FSA_3S.Services.Interface
{
    public interface IAuditService
    {
        Task<List<AuditDTO>> GetAuditInfoAsync();
    }
}