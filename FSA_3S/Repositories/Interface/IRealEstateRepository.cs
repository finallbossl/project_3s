using FSA_3S.DTOs;
using FSA_3S.Models.Entities;

namespace FSA_3S.Repositories.Interface
{
    public interface IRealEstateRepository
    {
        Task<RealEstateEntity> CreateAsync(RealEstateEntity entity);
        Task<RealEstateEntity?> GetByIdAsync(int id);
        Task<IEnumerable<RealEstateEntity>> GetAllAsync();
        /// <summary>
        /// API Put Realestate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RealEstateEntity?> GetByIdForPutAsync(int id);
        Task UpdateAsync(RealEstateEntity realEstate);
        /// <summary>
        /// API Delete RealEstate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(int id);
        Task<List<RealEstateBasicInfoDto>> GetRealEstateBasicInfoAsync();
    }
}