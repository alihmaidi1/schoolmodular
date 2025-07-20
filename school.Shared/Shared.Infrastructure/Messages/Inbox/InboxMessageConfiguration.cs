using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Domain.Entities.Message;

namespace Shared.Infrastructure.Messages.Inbox;

public class InboxMessageConfiguration: IEntityTypeConfiguration<InboxMessage>
{
    public void Configure(EntityTypeBuilder<InboxMessage> builder)
    {
        
        builder.ToTable("inbox_messages");
        
        builder.HasKey(outboxMessage => outboxMessage.Id);

        builder.Property(outBoxMessage => outBoxMessage.Content)
            .HasMaxLength(3000)
            .HasColumnType("jsonb");
    }
}