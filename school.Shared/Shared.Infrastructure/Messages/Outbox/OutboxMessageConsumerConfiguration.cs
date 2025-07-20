using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Domain.Entities.Message;

namespace Shared.Infrastructure.Messages.Outbox;

public class OutboxMessageConsumerConfiguration: IEntityTypeConfiguration<OutboxMessageConsumer>
{
    public void Configure(EntityTypeBuilder<OutboxMessageConsumer> builder)
    {
        builder.ToTable("OutboxMessageConsumers");
        builder.HasKey(x=>new {x.MessageId,x.Name});
        builder.Property(x => x.Name).HasMaxLength(500);
    }
}