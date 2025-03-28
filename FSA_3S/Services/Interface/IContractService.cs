using FSA_3S.Models.Requests;
using FSA_3S.Models.Respone;

namespace FSA_3S.Services.Interface
{
    public interface IContractService
    {
        Task<ContractResponse?> CreateContractAsync(ContractRequest request);
        Task<ContractResponse?> UpdateContractAsync(int contractId, ContractRequest request);
        Task<bool> DeleteContractAsync(int contractId);
        Task<List<ContractResponse>> GetAllContractsAsync();
    }
}