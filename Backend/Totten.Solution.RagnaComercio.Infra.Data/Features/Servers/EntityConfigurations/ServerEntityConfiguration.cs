﻿namespace Totten.Solution.RagnaComercio.Infra.Data.Features.Servers.EntityConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totten.Solution.RagnaComercio.Domain.Features.Servers;
using Totten.Solution.RagnaComercio.Infra.Data.Seeds;

internal class ServerEntityConfiguration : IEntityTypeConfiguration<Server>
{
    const string TABLE_NAME = "Servers";

    public void Configure(EntityTypeBuilder<Server> builder)
    {
        builder.ToTable(TABLE_NAME);
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.DisplayName).IsRequired();
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.UpdatedAt).IsRequired();
        builder.Property(e => e.IsActive).IsRequired();
        builder.Property(e => e.SiteUrl);

        builder.HasMany(e => e.Agents)
               .WithOne()
               .HasForeignKey(a => a.ServerId);

        builder.HasMany(e => e.Callbacks)
               .WithOne()
               .HasForeignKey(c => c.ServerId);

        builder.HasData(MyServerSeed.Seed());
    }
}
