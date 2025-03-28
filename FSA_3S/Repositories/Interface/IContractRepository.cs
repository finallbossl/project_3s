using FSA_3S.Models.Entities;

namespace FSA_3S.Repositories.Interface
{
    public interface IContractRepository
    {
        Task<ContractEntity?> GetContractByIdAsync(int contractId);
        Task<ContractEntity> AddContractAsync(ContractEntity contract);
        Task UpdateContractAsync(ContractEntity contract);

        Task AddMappingsAsync(List<MappingContractCustomerEntity> mappings);
        Task AddClauseMappingsAsync(List<MappingContractClauseEntity> clauseMappings);

        // Method Delete For PUT + Delete Contract
        Task DeleteContractAsync(ContractEntity contract);
        Task DeleteMappingsByContractIdAsync(int contractId);
        Task DeleteClauseMappingsByContractIdAsync(int contractId);
        //Method API Get All Contract
        Task<List<ContractEntity>> GetAllContractsAsync();
    }
}