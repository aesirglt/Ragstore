namespace Totten.Solution.Ragstore.Infra.Data.Features.Agents.EntityConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totten.Solution.Ragstore.Domain.Features.Agents;

public class UpdateTimeEntityConfiguration : IEntityTypeConfiguration<UpdateTime>
{
    const string TABLE_NAME = "UpdateTimes";

    public void Configure(EntityTypeBuilder<UpdateTime> builder)
    {
        builder.ToTable(TABLE_NAME);
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
    }
}
