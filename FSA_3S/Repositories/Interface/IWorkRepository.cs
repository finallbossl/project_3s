using FSA_3S.Models.Entities;

namespace FSA_3S.Repositories.Interface
{
    public interface IWorkRepository
    {
        Task AddWorkAsync(WorkEntity work);
        Task<IEnumerable<WorkEntity>> GetAllWorksAsync();
        Task<WorkEntity?> GetWorkByIdAsync(int workId);
        Task UpdateWorkAsync(WorkEntity work);
        Task DeleteWorkAsync(WorkEntity work);
    }
}