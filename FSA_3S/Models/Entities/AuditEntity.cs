using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSA_3S.Models.Entities
{
    [Table("audit")]
    public class AuditEntity
    {
        [Key]
        [Column("auditId")]
        public int AuditId { get; set; }

        [Column("entityType")]
        public string? EntityType { get; set; }

        [ForeignKey("Contract")]
        [Column("contractId")]
        public int? ContractId { get; set; }
        public ContractEntity? Contract { get; set; }

        [ForeignKey("RealEstate")]
        [Column("realEstateId")]
        public int? RealEstateId { get; set; }
        public RealEstateEntity? RealEstate { get; set; }

        [ForeignKey("Creator")]
        [Column("createdBy")]
        public int? CreatedBy { get; set; }
        public UserEntity? Creator { get; set; }

        [ForeignKey("Updater")]
        [Column("updatedBy")]
        public int? UpdatedBy { get; set; }
        public UserEntity? Updater { get; set; }

        [Column("createdAt")]
        public DateTime CreatedAt { get; set; }

        [Column("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [Column("isDeleted")]
        public bool? IsDeleted { get; set; }
    }
}