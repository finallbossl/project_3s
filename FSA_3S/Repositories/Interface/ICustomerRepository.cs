using FSA_3S.Models.Entities;

namespace FSA_3S.Repositories.Interface
{
    public interface ICustomerRepository
    {
        Task<CustomerEntity> CreateCustomerAsync(CustomerEntity customer);
        Task<IEnumerable<CustomerEntity>> GetAllCustomersAsync();
        Task<IEnumerable<CustomerEntity>> GetCustomersByTypeAsync(string customerType);
        /// <summary>
        /// 2 method sup contract below
        /// </summary>
        /// <param name="identityNumber"></param>
        /// <returns></returns>
        Task<CustomerEntity?> GetByIdentityNumberAsync(string identityNumber);
        Task<int> AddCustomerAsync(CustomerEntity customer);

    }
}
