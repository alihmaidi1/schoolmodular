using Autofac;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Serilog;
using Shared.Domain.Entities.Message;
using Shared.Domain.Event;
using Shared.Infrastructure.Serialization;

namespace Shared.Infrastructure.Messages;

public class MessageProcessor<TContext,TMessage,TEvent>
    (ILifetimeScope _lifetimeScope,
        IMediator mediator,
        ILogger logger
    )
    : BackgroundService where TContext : DbContext where TMessage : class,IMessage where TEvent : IEvent
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.Warning("Processing messages...");
                using var scope = _lifetimeScope.BeginLifetimeScope();
                var dbContext = scope.Resolve<TContext>();
                var Messages = await dbContext.Set<TMessage>()
                    .Where(m => m.ProcessedOn == null)
                    .ToListAsync(stoppingToken);
        
                foreach (var message in Messages)
                {
                    try
                    {
                        var eventType = Type.GetType(message.Type);
                        if (eventType == null)
                        {
                            logger.Warning("Could not resolve type: {Type}", message.Type);
                            continue;
                        }
                        var Event = JsonConvert.DeserializeObject<TEvent>(message.Content, SerializerSettings.Instance)!;
                
                        if (Event == null)
                        {
                            logger.Warning("Could not deserialize message: {Content}", message.Content);
                            continue;
                        }

                        await mediator.Publish(Event, stoppingToken);
                        message.ProcessedOn = DateTime.UtcNow;
        
                        logger.Information("Successfully processed  message with ID: {Id}", message.Id);
        
                    }
                    catch (Exception e)
                    {
                        message.ErrorMessage = e.Message;
                        
                    }
                    
                }
        
                await dbContext.SaveChangesAsync(stoppingToken);
                
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken); 
        }
    }
}