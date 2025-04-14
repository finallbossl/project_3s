using FSA_3S.Models.Entities;
using FSA_3S.Enum;

namespace FSA_3S.Repositories.Interface
{
    public interface IStaffRepository
    {
        /// <summary>
        /// API GET User role Staff
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<IEnumerable<UserEntity>> GetUsersByRoleAsync(string role);

        /// <summary>
        /// API Unable for accont role Staff
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<UnableStaffResponse> ToggleAccountStatusAsync(int userId);
        Task<UserEntity?> GetUserByIdAsync(int userId);
    }
}