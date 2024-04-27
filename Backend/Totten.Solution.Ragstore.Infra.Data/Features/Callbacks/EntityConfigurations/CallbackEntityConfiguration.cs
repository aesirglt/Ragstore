namespace Totten.Solution.Ragstore.Infra.Data.Features.Callbacks.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totten.Solution.Ragstore.Domain.Features.Callbacks;
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


        builder.HasData(new Callback
        {
            Id = 1,
            CallbackOwnerId = $"d7aeb595-44a5-4f5d-822e-980f35ace12d",
            Level = ECallbackType.SYSTEM,
            Name = "CallbackObscuro",
            CreatedAt = DateTime.Now,
            Server = "broTHOR",
            UpdatedAt = DateTime.Now,
            UserCellphone = "+5584988633251",
            ItemId = 490037,
            ItemPrice = 500_000_000
        });
    }
}