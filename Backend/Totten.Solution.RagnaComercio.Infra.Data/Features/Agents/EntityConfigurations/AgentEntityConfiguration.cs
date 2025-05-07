namespace Totten.Solution.RagnaComercio.Infra.Data.Features.Agents.EntityConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totten.Solution.RagnaComercio.Domain.Features.AgentAggregation;
using Totten.Solution.RagnaComercio.Infra.Data.Seeds;

public class AgentEntityConfiguration : IEntityTypeConfiguration<Agent>
{
    const string TABLE_NAME = "Agents";

    public void Configure(EntityTypeBuilder<Agent> builder)
    {
        builder.ToTable(TABLE_NAME);
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.UpdatedAt).IsRequired();
        builder.Property(e => e.IsActive).IsRequired();

        builder.HasOne(c => c.Server)
               .WithMany(s => s.Agents)
               .HasForeignKey(c => c.ServerId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(MyAgentSeed.Seed()); 
    }
}
