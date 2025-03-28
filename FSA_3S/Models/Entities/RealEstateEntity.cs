using FSA_3S.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSA_3S.Models.Entities
{
    [Table("realestate")]
    public class RealEstateEntity
    {
        [Key]
        [Column("realEstateId")]
        public int RealEstateId { get; set; }

        [Required]
        [Column("name")]
        [StringLength(50)]
        public required string RealEstateName { get; set; }

        [Required]
        [Column("type")]
        [StringLength(20)]
        public RealEstateTypeEnum RealEstateType { get; set; }

        [Required]
        [Column("status")]
        [StringLength(20)]
        public RealEstateStatusEnum RealEstateStatus { get; set; }

        [Column("price", TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        [ForeignKey(nameof(Customer))]
        [Column("sellerid")]
        public int Seller { get; set; }
        public CustomerEntity? Customer { get; set; }

        [Required]
        [Column("coordinates")]
        public string Coordinate { get; set; } = string.Empty;

        [Required]
        [Column("saledate")]
        public DateTime SaleDate { get; set; }

        [Required]
        [Column("image_path")]
        [StringLength(255)]
        public string? ImagePath { get; set; }

        [Column("address")]
        [StringLength(250)]
        public string? Address { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Required]
        [ForeignKey(nameof(Creator))]
        [Column("createdBy")]
        public int CreatedBy { get; set; }
        public UserEntity? Creator { get; set; }

        [ForeignKey(nameof(Updater))]
        [Column("updatedBy")]
        public int? UpdatedBy { get; set; }
        public UserEntity? Updater { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }

        [Column("updatedat")]
        public DateTime? UpdatedAt { get; set; }

        public List<ContractEntity> Contracts { get; set; } = new List<ContractEntity>();
    }
}