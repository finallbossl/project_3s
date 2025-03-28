using FSA_3S.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSA_3S.Data.Configuration
{
    public class RealEstateConfiguration : IEntityTypeConfiguration<RealEstateEntity>
    {
        public void Configure(EntityTypeBuilder<RealEstateEntity> builder)
        {
            builder.ToTable("realestate");

            builder.HasKey(r => r.RealEstateId);

            builder.Property(r => r.RealEstateName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(r => r.RealEstateType)
                   .HasMaxLength(10);

            builder.Property(r => r.RealEstateStatus)
                   .HasMaxLength(10);

            builder.Property(r => r.Price)
                   .IsRequired();

            builder.Property(r => r.Seller)
                .IsRequired()
                   .HasMaxLength(20);

            builder.Property(r => r.SaleDate)
                .IsRequired()
                   .HasMaxLength(20);

            builder.Property(r => r.Coordinate)
                   .HasMaxLength(20);

            builder.Property(r => r.ImagePath)
                   .HasMaxLength(255);

            builder.Property(r => r.Address)
                   .HasMaxLength(250);

            builder.Property(r => r.Description);

            builder.Property(r => r.CreatedAt)
                   .HasDefaultValueSql("CURRENT_DATE");

            builder.HasOne(r => r.Customer)
                   .WithMany(c => c.RealEstates)
                   .HasForeignKey(r => r.Seller)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}