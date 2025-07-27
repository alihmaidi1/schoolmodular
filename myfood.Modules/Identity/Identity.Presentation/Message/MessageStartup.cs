using Autofac;
using Identity.Domain;
using Identity.infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Shared.Domain.Entities.Message;
using Shared.Domain.Event;
using Shared.Infrastructure.Messages;

namespace Identity.Presentation.Message;

internal static class MessageStartup
{
    private static MessageProcessor<schoolIdentityDbContext, InboxMessage, IIntegrationEvent> _inboxmessageProcessor;
    private static MessageProcessor<schoolIdentityDbContext, OutboxMessage, IDomainEvent> _outboxmessageProcessor;

    public static async Task InitializeMessageProcessors()
    {
        var scope=IdentityCompositionRoot.BeginLifetimeScope();
        _outboxmessageProcessor = scope.Resolve<MessageProcessor<schoolIdentityDbContext, OutboxMessage, IDomainEvent>>();
        _inboxmessageProcessor = scope.Resolve<MessageProcessor<schoolIdentityDbContext, InboxMessage, IIntegrationEvent>>();

        await _outboxmessageProcessor.StartAsync(CancellationToken.None);
        await _inboxmessageProcessor.StartAsync(CancellationToken.None);
        
    }

    // public static async Task StopMessageProcessors()
    // {
    //     
    //     await _outboxmessageProcessor.StopAsync(CancellationToken.None);
    //     await _inboxmessageProcessor.StopAsync(CancellationToken.None);
    // }
    //
    
    

}