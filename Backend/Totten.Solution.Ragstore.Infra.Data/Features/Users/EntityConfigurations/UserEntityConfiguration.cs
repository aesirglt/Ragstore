namespace Totten.Solution.Ragstore.Infra.Data.Features.Users.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totten.Solution.Ragstore.Domain.Features.Users;

internal class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    const string TABLE_NAME = "Users";

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(TABLE_NAME);
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.UpdatedAt).IsRequired();

        builder.Property(e => e.Email).IsRequired();
        builder.Property(e => e.NormalizedEmail).IsRequired();
        builder.Property(e => e.PhoneNumber).IsRequired();
        builder.Property(e => e.IsActive).IsRequired();
        builder.Property(e => e.SearchCount).IsRequired();
        builder.Property(e => e.ReceivePriceAlerts).IsRequired();

        builder.HasMany(e => e.Callbacks)
            .WithOne()
            .HasForeignKey(x => x.CallbackOwnerId);

        builder.HasData(new User
        {
            Id = Guid.Parse("d7aeb595-44a5-4f5d-822e-980f35ace12d"),
            Name = "Aleff Moura",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            PhoneNumber = "+5584988633251",
            AvatarUrl = "",
            IsActive = true,
            Email = "aleffmds@gmail.com",
            NormalizedEmail = "ALEFFMDS@GMAIL.COM",
            ReceivePriceAlerts = true,
            SearchCount = 0,
        });
    }
}