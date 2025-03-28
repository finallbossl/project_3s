using FSA_3S.Models.Entities;

namespace FSA_3S.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<UserEntity?> GetByIdAsync(int userId);
        Task UpdateAsync(UserEntity user);
        Task<IEnumerable<UserEntity>> GetAllAsync();
    }
}
