using FSA_3S.Models.Entities;
using FSA_3S.Repositories.Interface;
using FSA_3S.Models;
using Microsoft.EntityFrameworkCore;

namespace FSA_3S.Repositories.Repository
{
    public class ContractRepository(AppDbContext context) : IContractRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<ContractEntity?> GetContractByIdAsync(int contractId)
        {
            return await _context.Contracts.FindAsync(contractId);
        }

        public async Task<ContractEntity> AddContractAsync(ContractEntity contract)
        {
            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();
            return contract;
        }
        /// <summary>
        /// Method for API Post COntract
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public async Task UpdateContractAsync(ContractEntity contract)
        {
            _context.Entry(contract).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task AddMappingsAsync(List<MappingContractCustomerEntity> mappings)
        {
            _context.MappingContractCustomers.AddRange(mappings);
            await _context.SaveChangesAsync();
        }

        public async Task AddClauseMappingsAsync(List<MappingContractClauseEntity> clauseMappings)
        {
            _context.MappingContractClauseEntities.AddRange(clauseMappings);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// API Get All Contract
        /// </summary>
        /// <returns></returns>
        public async Task<List<ContractEntity>> GetAllContractsAsync()
        {
            return await _context.Contracts
                .Include(c => c.MappingContractCustomer)
                    .ThenInclude(mc => mc.Buyer)
                .Include(c => c.MappingContractCustomer)
                    .ThenInclude(mc => mc.Seller)
                .Include(c => c.ContractClauses)
                .ToListAsync();
        }

        /// <summary>
        /// Method Delete support for API Put + Delete Contract
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public async Task DeleteMappingsByContractIdAsync(int contractId)
        {
            var existingMappings = _context.MappingContractCustomers.Where(m => m.ContractId == contractId);
            _context.MappingContractCustomers.RemoveRange(existingMappings);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClauseMappingsByContractIdAsync(int contractId)
        {
            var existingClauseMappings = _context.MappingContractClauseEntities.Where(m => m.ContractId == contractId);
            _context.MappingContractClauseEntities.RemoveRange(existingClauseMappings);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteContractAsync(ContractEntity contract)
        {
            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();
        }
        public async Task<List<int>> GetDeletedContractIdsAsync()
        {
            var deletedContracts = await _context.Audits
                .Where(a => a.IsDeleted == true && a.ContractId != null)
                .Select(a => a.ContractId.Value)
                .ToListAsync();

            Console.WriteLine($"[DEBUG] Deleted Contracts: {string.Join(", ", deletedContracts)}");

            return deletedContracts;
        }
    }
}