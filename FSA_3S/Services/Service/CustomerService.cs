using FSA_3S.Models.Entities;
using FSA_3S.Models.Requests;
using FSA_3S.Models.Respone;
using FSA_3S.Repositories.Interface;
using FSA_3S.Services.Interface;

namespace FSA_3S.Services.Service
{
    public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;

        public async Task<CustomerResponse> CreateCustomerAsync(CustomerRequest request)
        {
            var customer = new CustomerEntity
            {
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                Gender = request.Gender,
                CCCD = request.CCCD,
                CustomerType = request.CustomerType,
                //Notes = request.Notes,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _customerRepository.CreateCustomerAsync(customer);

            return new CustomerResponse
            {
                CustomerId = result.CustomerId,
                FullName = result.FullName,
                Email = result.Email,
                PhoneNumber = result.PhoneNumber,
                Address = result.Address,
                Gender = result.Gender,
                CCCD = result.CCCD,
                CustomerType = result.CustomerType,
                //Notes = result.Notes,
                CreatedAt = result.CreatedAt
            };
        }

        public async Task<IEnumerable<CustomerResponse>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();

            return customers.Select(c => new CustomerResponse
            {
                CustomerId = c.CustomerId,
                FullName = c.FullName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                Address = c.Address,
                Gender = c.Gender,
                CCCD = c.CCCD,
                CustomerType = c.CustomerType,
                //Notes = c.Notes,
                CreatedAt = c.CreatedAt
            });
        }

        public async Task<IEnumerable<CustomerResponse>> GetCustomersByTypeAsync(string customerType)
        {
            var customers = await _customerRepository.GetCustomersByTypeAsync(customerType);

            return customers.Select(c => new CustomerResponse
            {
                CustomerId = c.CustomerId,
                FullName = c.FullName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                Address = c.Address,
                Gender = c.Gender,
                CCCD = c.CCCD,
                CustomerType = c.CustomerType,
                //Notes = c.Notes,
                CreatedAt = c.CreatedAt
            });
        }
    }
}
