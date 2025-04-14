using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FSA_3S.Models.Entities;

namespace FSA_3S.Data.Configuration
{
    public class AuditConfiguration : IEntityTypeConfiguration<AuditEntity>
    {
        public void Configure(EntityTypeBuilder<AuditEntity> builder)
        {
            builder.ToTable("Audit");

            builder.HasKey(a => a.AuditId);

            builder.Property(a => a.EntityType)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(a => a.RealEstate)
                .WithMany(r => r.Audits)
                .HasForeignKey(a => a.RealEstateId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Contract)
                .WithMany()
                .HasForeignKey(a => a.ContractId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}