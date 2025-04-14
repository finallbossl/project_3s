using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FSA_3S.Models.Entities;

namespace FSA_3S.Data.Configuration
{
    public class ContractConfiguration : IEntityTypeConfiguration<ContractEntity>
    {
        public void Configure(EntityTypeBuilder<ContractEntity> builder)
        {
            builder.ToTable("contract");

            builder.HasKey(e => e.ContractId);

            builder.Property(e => e.ContractType)
                .HasColumnName("contracttype")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.ContractStatus)
                .HasColumnName("contractstatus")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.StatusPayment)
    .HasColumnName("statuspayment")
    .HasMaxLength(50)
    .IsRequired();

            builder.Property(e => e.StartDate)
                .HasColumnName("startdate")
                .IsRequired(false);

            builder.Property(e => e.EndDate)
                .HasColumnName("enddate")
                .IsRequired(false);

            builder.Property(e => e.CreatedBy)
                .HasColumnName("createdby")
                .IsRequired();

            builder.Property(e => e.UpdatedBy)
                .HasColumnName("updatedby")
                .IsRequired(false);

            builder.Property(e => e.CreatedAt)
                .HasColumnName("createdat")
                .IsRequired();

            builder.Property(e => e.UpdatedAt)
                .HasColumnName("updatedat")
                .IsRequired(false);

            builder.HasOne(e => e.RealEstate)
                .WithMany(e => e.Contracts)
                .HasForeignKey(e => e.RealEstateId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Creator)
                .WithMany()
                .HasForeignKey(e => e.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Updater)
                .WithMany()
                .HasForeignKey(e => e.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(e => e.ContractClauses)
                .WithOne(mc => mc.Contract)
                .HasForeignKey(mc => mc.ContractId);
        }
    }
}