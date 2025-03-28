using FSA_3S.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSA_3S.Models.Entities
{
    [Table("mappingcontractcustomer")]
    public class MappingContractCustomerEntity
    {
        [Key]
        [Column("mappingContractCustomerId")]
        public int MappingContractCustomerId { get; set; }

        [ForeignKey("Contract")]
        [Column("contractId")]
        public int ContractId { get; set; }
        public ContractEntity Contract { get; set; } = null!;

        [ForeignKey("Buyer")]
        [Column("buyerId")]
        public int? BuyerId { get; set; }
        public CustomerEntity? Buyer { get; set; }

        [ForeignKey("Seller")]
        [Column("sellerId")]
        public int? SellerId { get; set; }
        public CustomerEntity? Seller { get; set; }

        //[Required]
        //[Column("customertype")]
        //[EnumDataType(typeof(CustomerTypeEnum))]
        //public CustomerTypeEnum CustomerType { get; set; }
    }
}