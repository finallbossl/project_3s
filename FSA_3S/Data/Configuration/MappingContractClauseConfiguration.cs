using FSA_3S.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSA_3S.Data.Configuration
{
    public class MappingContractClauseConfiguration : IEntityTypeConfiguration<MappingContractClauseEntity>
    {
        public void Configure(EntityTypeBuilder<MappingContractClauseEntity> builder)
        {
            builder.ToTable("mappingcontractclause");

            builder.HasKey(e => new { e.ContractId, e.ClauseId });

            builder.HasOne(e => e.Contract)
                   .WithMany(e => e.ContractClauses)
                   .HasForeignKey(e => e.ContractId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Clause)
                   .WithMany(e => e.ContractClauses)
                   .HasForeignKey(e => e.ClauseId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}