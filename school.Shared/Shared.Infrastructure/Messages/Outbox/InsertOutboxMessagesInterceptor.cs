using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using Shared.Domain.Entities;
using Shared.Domain.Entities.Message;
using Shared.Infrastructure.Serialization;

namespace Shared.Infrastructure.Messages.Outbox;

public class InsertOutboxMessagesInterceptor: SaveChangesInterceptor
{

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if(eventData.Context is not null) 
            InsertOutboxMessages(eventData.Context);
        
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    private static void InsertOutboxMessages(DbContext context)
    {
        var domainEvents = context
            .ChangeTracker
            
            .Entries<IAggregate>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.GetDomainEvents();
                
                entity.ClearDomainEvents();
                return domainEvents;
            }).ToList();
        var outboxMessages=domainEvents.Select(domainEvent => new OutboxMessage
            {
                Id = domainEvent.EventId,
                Type = domainEvent.GetType().AssemblyQualifiedName,
                Content = JsonConvert.SerializeObject(domainEvent,SerializerSettings.Instance),
                CreatedOn = domainEvent.OccurredOn,
            })
            .ToList();
        
        context.Set<OutboxMessage>().AddRange(outboxMessages);
    }
    
    
    
}