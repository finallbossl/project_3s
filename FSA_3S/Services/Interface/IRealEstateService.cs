using FSA_3S.Models.Entities;
using FSA_3S.Models.Requests;
using FSA_3S.Models.Respone;

namespace FSA_3S.Services.Interface
{
    public interface IRealEstateService
    {
        /// <summary>
        /// Method for API Post RealEstate
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<RealEstateRespone?> CreateAsync(RealEstateRequest request);
        Task<RealEstateEntity?> GetByIdAsync(int id);
        /// <summary>
        /// Method for API Get All Data from RealEstate
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<RealEstateRespone>> GetAllRealEstateAsync();
        /// <summary>
        /// API Put RealEstate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RealEstateRespone> UpdateRealEstateAsync(int id, RealEstateRequest request);
        /// <summary>
        /// api Delete RealEstate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> DeleteRealEstateAsync(int id);
    }
}