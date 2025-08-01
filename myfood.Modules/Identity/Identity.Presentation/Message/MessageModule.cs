using Autofac;
using Identity.infrastructure;
using Microsoft.Extensions.Hosting;
using Shared.Domain.Entities.Message;
using Shared.Domain.Event;
using Shared.Infrastructure.Messages;

namespace Identity.Presentation.Message;

public class MessageModule: Module 
{
    protected override void Load(ContainerBuilder builder)
    {
        
        builder.RegisterType<MessageProcessor<SchoolIdentityDbContext,OutboxMessage,IDomainEvent>>()
            .AsSelf()
            .SingleInstance();
        builder.RegisterType<MessageProcessor<SchoolIdentityDbContext,InboxMessage,IIntegrationEvent>>()
            .AsSelf()
            .SingleInstance();

    }

}