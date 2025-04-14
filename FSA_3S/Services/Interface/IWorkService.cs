using FSA_3S.Models.Entities;
using FSA_3S.Models.Requests;
using FSA_3S.Models.Respone;

namespace FSA_3S.Services.Interface
{
    public interface IWorkService
    {
        Task<WorkEntity?> CreateWorkAsync(WorkRequest request);
        Task<IEnumerable<WorkResponse>> GetAllWorksAsync();
        Task<WorkEntity?> UpdateWorkAsync(int workId, WorkRequest request);
        Task<bool> DeleteWorkAsync(int workId);
    }
}