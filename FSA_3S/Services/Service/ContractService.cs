using FSA_3S.Enum;
using FSA_3S.Helpers;
using FSA_3S.Models.Entities;
using FSA_3S.Models.Requests;
using FSA_3S.Models.Respone;
using FSA_3S.Repositories.Interface;
using FSA_3S.Repositories.Repository;
using FSA_3S.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FSA_3S.Services.Service
{
    public class ContractService(
        IContractRepository contractRepository,
        ICustomerRepository customerRepository,
        IRealEstateRepository realEstateRepository,
        IHttpContextAccessor httpContextAccessor) : IContractService
    {
        private readonly IContractRepository _contractRepository = contractRepository;
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IRealEstateRepository _realEstateRepository = realEstateRepository;

        /// <summary>
        /// APi Post Contract
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public async Task<ContractResponse?> CreateContractAsync(ContractRequest request)
        {
            int createdBy = UserIdHelper.GetUserId(_httpContextAccessor)
                ?? throw new UnauthorizedAccessException("Invalid or missing user ID.");

            int buyerId = await GetOrCreateCustomerAsync(request.Buyer);
            int sellerId = await GetOrCreateCustomerAsync(request.Seller);

            var realEstates = await _realEstateRepository.GetRealEstateBasicInfoAsync();
            //var realEstate = realEstates.FirstOrDefault(r => r.RealEstateId == request.RealEstateId)
            //    ?? throw new KeyNotFoundException("Real estate property not found.");

            //if (realEstate.Seller != sellerId)
            //{
            //    throw new UnauthorizedAccessException("Seller ID does not match the owner of the real estate.");
            //}

            var contract = new ContractEntity
            {
                RealEstateId = request.RealEstateId,
                ContractType = request.ContractType,
                ContractStatus = request.ContractStatus,
                StatusPayment = request.StatusPayment,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                CreatedBy = createdBy,
                UpdatedBy = null,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
            };
            var createdContract = await _contractRepository.AddContractAsync(contract);

            var mappings = new List<MappingContractCustomerEntity>
    {
        new()
        {
            ContractId = createdContract.ContractId,
            BuyerId = buyerId,
            SellerId = sellerId,
        },
    };

            await _contractRepository.AddMappingsAsync(mappings);

            var clauseMappings = request.ClauseIds.Select(clauseId => new MappingContractClauseEntity
            {
                ContractId = createdContract.ContractId,
                ClauseId = clauseId
            }).ToList();

            await _contractRepository.AddClauseMappingsAsync(clauseMappings);

            return new ContractResponse
            {
                ContractId = createdContract.ContractId,
                RealEstateId = createdContract.RealEstateId,
                BuyerId = buyerId,
                SellerId = sellerId,
                ContractType = createdContract.ContractType,
                ContractStatus = createdContract.ContractStatus,
                StatusPayment = createdContract.StatusPayment,
                StartDate = createdContract.StartDate,
                EndDate = createdContract.EndDate,
                ClauseIds = request.ClauseIds,
                CreatedBy = createdContract.CreatedBy,
                CreatedAt = createdContract.CreatedAt
            };
        }
        private async Task<int> GetOrCreateCustomerAsync(PersonInfo personInfo)
        {
            var existingCustomer = await _customerRepository.GetByIdentityNumberAsync(personInfo.CCCD);
            if (existingCustomer != null)
                return existingCustomer.CustomerId;

            var newCustomer = new CustomerEntity
            {
                FullName = personInfo.FullName,
                CCCD = personInfo.CCCD,
                PhoneNumber = personInfo.PhoneNumber,
                Address = personInfo.Address,
                CreatedAt = DateTime.UtcNow
            };

            return await _customerRepository.AddCustomerAsync(newCustomer);
        }
        /// <summary>
        /// API Get All Contract
        /// </summary>
        /// <returns></returns>
        public async Task<List<ContractResponse>> GetAllContractsAsync()
        {
            var contracts = await _contractRepository.GetAllContractsAsync();
            return contracts.Select(c => new ContractResponse
            {
                ContractId = c.ContractId,
                RealEstateId = c.RealEstateId,
                BuyerId = c.MappingContractCustomer.FirstOrDefault(mc => mc.Buyer != null)?.Buyer.CustomerId ?? 0,
                SellerId = c.MappingContractCustomer.FirstOrDefault(mc => mc.Seller != null)?.Seller.CustomerId ?? 0,
                ContractType = c.ContractType,
                ContractStatus = c.ContractStatus,
                StatusPayment = c.StatusPayment,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                ClauseIds = c.ContractClauses.Select(cl => cl.ClauseId).ToList(),
                CreatedBy = c.CreatedBy,
                UpdatedBy = c.UpdatedBy,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            }).ToList();
        }

        /// <summary>
        /// API Put Contract
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public async Task<ContractResponse?> UpdateContractAsync(int contractId, ContractRequest request)
        {
            var contract = await _contractRepository.GetContractByIdAsync(contractId);
            if (contract == null)
                return null;

            // Cập nhật thông tin Buyer và Seller
            int buyerId = await GetOrCreateCustomerAsync(request.Buyer);
            int sellerId = await GetOrCreateCustomerAsync(request.Seller);
            var realEstates = await _realEstateRepository.GetRealEstateBasicInfoAsync();
            //var realEstate = realEstates.FirstOrDefault(r => r.RealEstateId == request.RealEstateId)
            //    ?? throw new KeyNotFoundException("Real estate property not found.");

            //if (realEstate.Seller != sellerId)
            //{
            //    throw new UnauthorizedAccessException("Seller ID does not match the owner of the real estate.");
            //}

            // Cập nhật thông tin hợp đồng
            contract.RealEstateId = request.RealEstateId;
            contract.ContractType = request.ContractType;
            contract.ContractStatus = request.ContractStatus;
            contract.StatusPayment = request.StatusPayment;
            contract.StartDate = request.StartDate;
            contract.EndDate = request.EndDate;
            contract.UpdatedBy = UserIdHelper.GetUserId(_httpContextAccessor)
                ?? throw new UnauthorizedAccessException("Invalid or missing user ID.");
            contract.UpdatedAt = DateTime.UtcNow;


            //if (contract.RealEstateId == null)
            //{
            //    throw new KeyNotFoundException("Real estate property not found.");
            //}

            //if (contract.RealEstateId != C) // Chắc chắn SellerId là kiểu int?
            //{
            //    throw new UnauthorizedAccessException("Seller ID does not match the owner of the real estate.");
            //}
            await _contractRepository.UpdateContractAsync(contract);

            // Xóa dữ liệu cũ trước khi thêm mới
            await _contractRepository.DeleteMappingsByContractIdAsync(contractId);
            await _contractRepository.DeleteClauseMappingsByContractIdAsync(contractId);

            var mappings = new List<MappingContractCustomerEntity>
    {
        new()
        {
            ContractId = contract.ContractId,
            BuyerId = buyerId,
            SellerId = sellerId,
            //CustomerType = CustomerTypeEnum.Buyer
        },
        //new()
        //{
        //    ContractId = contract.ContractId,
        //    SellerId = sellerId,
        //    CustomerType = CustomerTypeEnum.Seller
        //}
    };

            await _contractRepository.AddMappingsAsync(mappings);

            var clauseMappings = request.ClauseIds.Select(clauseId => new MappingContractClauseEntity
            {
                ContractId = contract.ContractId,
                ClauseId = clauseId
            }).ToList();

            await _contractRepository.AddClauseMappingsAsync(clauseMappings);

            return new ContractResponse
            {
                ContractId = contract.ContractId,
                RealEstateId = contract.RealEstateId,
                BuyerId = buyerId,
                SellerId = sellerId,
                ContractType = contract.ContractType,
                ContractStatus = contract.ContractStatus,
                StatusPayment = contract.StatusPayment,
                StartDate = contract.StartDate,
                EndDate = contract.EndDate,
                ClauseIds = request.ClauseIds,
                CreatedBy = contract.CreatedBy,
                CreatedAt = contract.CreatedAt,
                UpdatedBy = contract.UpdatedBy,
                UpdatedAt = contract.UpdatedAt
            };
        }
        /// <summary>
        /// API Delete Contract
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteContractAsync(int contractId)
        {
            var contract = await _contractRepository.GetContractByIdAsync(contractId);
            if (contract == null)
                return false;

            // Xóa các bảng mapping trước khi xóa contract
            await _contractRepository.DeleteMappingsByContractIdAsync(contractId);
            await _contractRepository.DeleteClauseMappingsByContractIdAsync(contractId);

            // Xóa hợp đồng
            await _contractRepository.DeleteContractAsync(contract);

            return true;
        }
    }
}