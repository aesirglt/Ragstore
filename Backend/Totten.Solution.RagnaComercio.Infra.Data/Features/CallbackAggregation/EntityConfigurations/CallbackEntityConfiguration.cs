namespace Totten.Solution.RagnaComercio.Infra.Data.Features.CallbackAggregation.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totten.Solution.RagnaComercio.Domain.Features.CallbackAggregation;

internal class CallbackEntityConfiguration : IEntityTypeConfiguration<Callback>
{
    const string TABLE_NAME = "Callbacks";

    public void Configure(EntityTypeBuilder<Callback> builder)
    {
        builder.ToTable(TABLE_NAME);
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.UpdatedAt).IsRequired();
        builder.Property(e => e.ItemPrice).IsRequired();
        builder.Property(e => e.ItemId).IsRequired();

        builder.HasOne(c => c.Server)
               .WithMany(s => s.Callbacks)
               .HasForeignKey(c => c.ServerId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.User)
               .WithMany(s => s.Callbacks)
               .HasForeignKey(c => c.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}