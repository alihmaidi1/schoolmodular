using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Domain.Entities.Message;

namespace Shared.Infrastructure.Messages.Inbox;

public class InboxMessageConsumerConfiguration: IEntityTypeConfiguration<InboxMessageConsumer>
{
    public void Configure(EntityTypeBuilder<InboxMessageConsumer> builder)
    {
        
        builder.ToTable("InboxMessageConsumers");
        builder.HasKey(x=>new {x.MessageId,x.Name});
        builder.Property(x => x.Name).HasMaxLength(500);
    }
}