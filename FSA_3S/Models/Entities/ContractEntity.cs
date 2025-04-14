using FSA_3S.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSA_3S.Models.Entities
{
    [Table("contract")]
    public class ContractEntity
    {
        [Key]
        [Column("contractId")]
        public int ContractId { get; set; }

        [ForeignKey("RealEstate")]
        [Column("realEstateId")]
        public int? RealEstateId { get; set; }
        public RealEstateEntity? RealEstate { get; set; }

        [Column("contracttype")]
        public ContractTypeEnum ContractType { get; set; }

        [Column("contractstatus")]
        public ContractStatusEnum ContractStatus { get; set; }

        [Required]
        [Column("statuspayment")]
        public StatusPaymentEnum StatusPayment { get; set; }

        [Column("startdate")]
        public DateTime? StartDate { get; set; }

        [Column("enddate")]
        public DateTime? EndDate { get; set; }

        [ForeignKey("Creator")]
        [Column("createdBy")]
        public int CreatedBy { get; set; }
        public UserEntity? Creator { get; set; }

        [ForeignKey("Updater")]
        [Column("updatedBy")]
        public int? UpdatedBy { get; set; }
        public UserEntity? Updater { get; set; }

        [Column("createdAt")]
        public DateTime CreatedAt { get; set; }

        [Column("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        public List<MappingContractCustomerEntity> MappingContractCustomer { get; set; } = [];
        public List<MappingContractClauseEntity> ContractClauses { get; set; } = [];
    }
}