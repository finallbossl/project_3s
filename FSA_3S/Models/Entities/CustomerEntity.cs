using FSA_3S.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSA_3S.Models.Entities
{
    [Table("customer")]
    public class CustomerEntity
    {
        [Key]
        [Column("customerId")]
        public int CustomerId { get; set; }

        [Required]
        [Column("fullname")]
        [StringLength(50)]
        public required string FullName { get; set; }
        [Column("gender")]
        [StringLength(10)]
        public string? Gender { get; set; }
     
        [Column("email")]
        [StringLength(50)]
        public string? Email { get; set; }

        [Column("phonenumber")]
        [StringLength(13)]
        public string? PhoneNumber { get; set; }

        [Column("address")]
        [StringLength(250)]
        public string? Address { get; set; }
        [Column("CCCD")]
        [StringLength(20)]
        public string? CCCD { get; set; }

        [Column("customertype")]
        [StringLength(10)]
        public CustomerTypeEnum CustomerType { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }

        public List<RealEstateEntity> RealEstates { get; set; } = [];

        [InverseProperty("Buyer")]
        public List<MappingContractCustomerEntity> BuyerMappings { get; set; } = [];

        [InverseProperty("Seller")]
        public List<MappingContractCustomerEntity> SellerMappings { get; set; } = [];

        public List<AppointmentEntity>? Appointments { get; set; }

    }
}