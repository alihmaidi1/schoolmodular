using MassTransit;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shared.Domain.Entities.Message;
using Shared.Domain.Event;
using Shared.Infrastructure.Serialization;

namespace Shared.Infrastructure.Messages.Inbox;

public class IntegrationEventConsumer<TIntegrationEvent, TDbContext>:  IConsumer<TIntegrationEvent> 
     where TIntegrationEvent : class,IIntegrationEvent where TDbContext : DbContext
{
     
     private readonly TDbContext  _dbContext;

     public IntegrationEventConsumer(TDbContext dbContext)
     {
         _dbContext= dbContext;
         
     }
     
     public async Task Consume(ConsumeContext<TIntegrationEvent> context)
     {
         
         var integrationEvent = context.Message;
         var eventType = integrationEvent.GetType().AssemblyQualifiedName;
         var isExists=await _dbContext.Set<InboxMessage>().AnyAsync(x=>x.Id==integrationEvent.EventId&&x.Type==eventType);
         if (isExists)
         {
             return;
         }
         var inboxMessage = new InboxMessage()
         {
             Id = integrationEvent.EventId,
             Type = eventType,
             Content = JsonConvert.SerializeObject(integrationEvent, SerializerSettings.Instance),
             ProcessedOn = integrationEvent.OccurredOn

         };
         _dbContext.Set<InboxMessage>().Add(inboxMessage);
         await _dbContext.SaveChangesAsync(context.CancellationToken);
     }
}