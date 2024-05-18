namespace Totten.Solution.Ragstore.Infra.Data.Features.Agents.EntityConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totten.Solution.Ragstore.Domain.Features.AgentAggregation;

public class UpdateTimeEntityConfiguration : IEntityTypeConfiguration<Agent>
{
    const string TABLE_NAME = "UpdateTimes";

    public void Configure(EntityTypeBuilder<Agent> builder)
    {
        builder.ToTable(TABLE_NAME);
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.UpdatedAt).IsRequired();
        builder.Property(e => e.IsActive).IsRequired();
    }
}
