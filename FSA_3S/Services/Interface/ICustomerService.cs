using FSA_3S.Models.Entities;
using FSA_3S.Models.Requests;
using FSA_3S.Models.Respone;

namespace FSA_3S.Services.Interface
{
    public interface ICustomerService
    {
        Task<CustomerResponse> CreateCustomerAsync(CustomerRequest request);
        Task<IEnumerable<CustomerResponse>> GetAllCustomersAsync();
        Task<IEnumerable<CustomerResponse>> GetCustomersByTypeAsync(string customerType);
    }
}
