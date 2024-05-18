namespace Totten.Solution.Ragstore.Infra.Data.Features.Servers.EntityConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totten.Solution.Ragstore.Domain.Features.Servers;
using Totten.Solution.Ragstore.Infra.Data.Seeds;

internal class ServerEntityConfiguration : IEntityTypeConfiguration<Server>
{
    const string TABLE_NAME = "Servers";
    public void Configure(EntityTypeBuilder<Server> builder)
    {
        builder.ToTable(TABLE_NAME);
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.UpdatedAt).IsRequired();
        builder.Property(e => e.IsActive).IsRequired();
        builder.Property(e => e.SiteUrl);

        builder.HasData(MyServerSeed.Seed());
    }
}
