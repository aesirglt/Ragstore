namespace Totten.Solution.Ragstore.Infra.Data.Features.CallbackAggregation.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totten.Solution.Ragstore.Domain.Features.CallbackAggregation;
internal class CallbackScheduleConfiguration : IEntityTypeConfiguration<CallbackSchedule>
{
    const string TABLE_NAME = "CallbacksSchedule";

    public void Configure(EntityTypeBuilder<CallbackSchedule> builder)
    {
        builder.ToTable(TABLE_NAME);
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.Sended).IsRequired();
        builder.Property(e => e.SendIn).IsRequired();
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.UpdatedAt).IsRequired();
    }
}