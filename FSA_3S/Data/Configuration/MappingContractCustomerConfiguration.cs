using FSA_3S.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSA_3S.Data.Configuration
{
    public class MappingContractCustomerConfiguration : IEntityTypeConfiguration<MappingContractCustomerEntity>
    {
        public void Configure(EntityTypeBuilder<MappingContractCustomerEntity> builder)
        {
            builder.ToTable("mappingcontractcustomer");

            builder.HasKey(e => e.MappingContractCustomerId);

            builder.Property(e => e.MappingContractCustomerId)
                   .HasColumnName("mappingContractCustomerId")
                   .IsRequired();

            // Cascade khi xóa Contract
            builder.HasOne(e => e.Contract)
                   .WithMany(e => e.MappingContractCustomer)
                   .HasForeignKey(e => e.ContractId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Không cascade khi xóa Customer
            builder.HasOne(e => e.Seller)
                   .WithMany(e => e.SellerMappings)
                   .HasForeignKey(e => e.SellerId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Buyer)
       .WithMany(e => e.BuyerMappings)
       .HasForeignKey(e => e.BuyerId)
       .OnDelete(DeleteBehavior.NoAction);

            // Dùng để xác định vai trò người mua hoặc người bán
            //builder.Property(e => e.CustomerType)
            //       .HasColumnName("customertype")
            //       .HasMaxLength(10)
            //       .IsRequired();
        }
    }
}