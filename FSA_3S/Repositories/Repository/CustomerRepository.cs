using FSA_3S.Enum;
using FSA_3S.Models;
using FSA_3S.Models.Entities;
using FSA_3S.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace FSA_3S.Repositories.Repository
{
    public class CustomerRepository(AppDbContext context) : ICustomerRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<CustomerEntity> CreateCustomerAsync(CustomerEntity customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<IEnumerable<CustomerEntity>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<IEnumerable<CustomerEntity>> GetCustomersByTypeAsync(string customerType)
        {
            if (!System.Enum.TryParse<CustomerTypeEnum>(customerType, true, out var parsedCustomerType))
            {
                throw new ArgumentException($"Invalid customer type: {customerType}");
            }

            return await _context.Customers
                .Where(c => c.CustomerType == parsedCustomerType)
                .ToListAsync();
        }

        public async Task<CustomerEntity?> GetByIdentityNumberAsync(string identityNumber)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.CCCD == identityNumber);
        }
        /// <summary>
        /// 2 method sup for contract (logic)
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<int> AddCustomerAsync(CustomerEntity customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer.CustomerId;
        }
    }
}