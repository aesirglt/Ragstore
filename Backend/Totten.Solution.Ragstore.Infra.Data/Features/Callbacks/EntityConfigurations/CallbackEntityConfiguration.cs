namespace Totten.Solution.Ragstore.Infra.Data.Features.Callbacks.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totten.Solution.Ragstore.Domain.Features.Callbacks;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using ItemId = int;
using ItemPrice = double;
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
        
        builder.Property(e => e.Items)
                   .HasConversion(
                    values => values.Count == 0
                        ? string.Empty
                        : string.Join('#', values.Select(item => $"{item.Key}:{item.Value}")),
                    str => string.IsNullOrEmpty(str)
                        ? new Dictionary<ItemId, ItemPrice>()
                        : str.Split('#', StringSplitOptions.RemoveEmptyEntries)
                             .Select(item =>
                                     item.Split(':', StringSplitOptions.RemoveEmptyEntries)
                                         .Apply(keyPar => new KeyValuePair<ItemId, ItemPrice>(int.Parse(keyPar[0]), double.Parse(keyPar[1]))))
                             .ToDictionary());

        builder.HasData(new Callback
        {
            Id = 1,
            CallbackOwnerId = $"d7aeb595-44a5-4f5d-822e-980f35ace12d",
            Level = ECallbackType.SYSTEM,
            Name = "MasterCallback",
            CreatedAt = DateTime.Now,
            Server = "bro-THOR",
            UpdatedAt = DateTime.Now,
            UserCellphone = "+5584988633251",
            Items =
            {
                { 490037, 500_000_000 }
            }
        });
    }
}