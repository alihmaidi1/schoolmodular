using Mapster;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Entities.Message;
using Shared.Domain.Event;

namespace Shared.Application.Message;

public   class IdempotentEventHandler<TDbContext,TMessageConsumer,TEvent>:IEventHandler<TEvent> where TEvent : IEvent
where TDbContext : DbContext where TMessageConsumer : class,IMessageConsumer
{
    private readonly IEventHandler<TEvent> _decorated;
    private readonly TDbContext  _dbContext;
    public IdempotentEventHandler(TDbContext  dbContext,IEventHandler<TEvent> decorated)
    {
        _decorated = decorated;
        _dbContext = dbContext;
    }
    
    
    public async Task Handle(TEvent tEvent, CancellationToken cancellationToken)
    {
        var isProccessed=await _dbContext.Set<TMessageConsumer>().AnyAsync(x=>x.MessageId==tEvent.EventId&&x.Name==_decorated.GetType().Name);
        if (isProccessed) return;
        await _decorated.Handle(tEvent, cancellationToken);
        var messageConsumer = new MessageConsumer(tEvent.EventId, _decorated.GetType().Name!);
        var TmessageConsumer = messageConsumer.Adapt<TMessageConsumer>();
        _dbContext.Set<TMessageConsumer>().Add(TmessageConsumer);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}