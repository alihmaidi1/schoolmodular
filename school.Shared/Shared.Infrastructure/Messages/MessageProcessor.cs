using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.Domain.Entities;
using Shared.Domain.Entities.Message;
using Shared.Domain.Event;
using Shared.Infrastructure.Serialization;

namespace Shared.Infrastructure.Messages;

public class MessageProcessor<TContext,TMessage,TEvent>
    (IServiceProvider serviceProvider, IEventDispatcher bus, ILogger<MessageProcessor<TContext,TMessage,TEvent>> logger)
    : BackgroundService where TContext : DbContext where TMessage : class,IMessage where TEvent : IEvent
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        
        while (!stoppingToken.IsCancellationRequested)
        {
                using var scope = serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();
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
                            logger.LogWarning("Could not resolve type: {Type}", message.Type);
                            continue;
                        }
                        var Event = JsonConvert.DeserializeObject<TEvent>(message.Content, SerializerSettings.Instance)!;
                
                        if (Event == null)
                        {
                            logger.LogWarning("Could not deserialize message: {Content}", message.Content);
                            continue;
                        }

                        await bus.DispatchAsync(Event,CancellationToken.None);

                        message.ProcessedOn = DateTime.UtcNow;

                        logger.LogInformation("Successfully processed  message with ID: {Id}", message.Id);

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