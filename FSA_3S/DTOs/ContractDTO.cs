using FSA_3S.Enum;

namespace FSA_3S.DTOs
{
    public class ContractDTO
    {
        public int ContractId { get; set; }
        public int? RealEstateId { get; set; }
        public int? CustomerId { get; set; }
        public ContractTypeEnum ContractType { get; set; }
        public ContractStatusEnum ContractStatus { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}