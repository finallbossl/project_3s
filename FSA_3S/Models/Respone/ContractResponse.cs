
using FSA_3S.Enum;
using FSA_3S.Models.Entities;

namespace FSA_3S.Models.Respone
{
    public class ContractResponse
    {
        public int ContractId { get; set; }
        public int? RealEstateId { get; set; }
        public required int BuyerId { get; set; }
        public required int SellerId { get; set; }
        public ContractTypeEnum ContractType { get; set; }
        public ContractStatusEnum ContractStatus { get; set; }
        public StatusPaymentEnum StatusPayment { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public required List<int> ClauseIds { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}