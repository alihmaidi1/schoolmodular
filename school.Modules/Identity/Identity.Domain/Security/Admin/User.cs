using Identity.Domain.Event;
using Microsoft.AspNetCore.Identity;
using Shared.Domain.Entities;
using Shared.Domain.Event;
using Shared.Domain.OperationResult;

namespace Identity.Domain.Security.Admin;

public class User: IdentityUser<Guid>, IAggregate
{

    public User()
    {
        
        Id=Guid.NewGuid();
    }
    public UserType UserType { get; set; }=UserType.Student;


    public string? ForgetCode { get; private set; }
    
    public DateTime? ForgetDate { get; private set; }

    public string CreatedBy { get; set; } = "";
    public DateTime CreatedAt { get; set; }=DateTime.UtcNow;
    public string LastModifiedBy { get; set; } = "";
    public DateTime LastModified { get; set; }=DateTime.UtcNow;
    public void SetCreatedBy(string newBy)
    {
        CreatedAt=DateTime.UtcNow;
        CreatedBy=newBy;
    }

    public void SetModified(string newBy)
    {
        LastModifiedBy=newBy;
        LastModified=DateTime.UtcNow;
    }
    public void ClearForgetCode()
    {
        ForgetCode = null;
        ForgetDate = null;
    }



    public TResult<bool> SetForgetCode(string code,int forgetMinute)
    {
        if (forgetMinute<=0)
        {
            return Result.InternalError<bool>(Error.Internal("forget minute must be greater than zero"));
        }
        ForgetCode=code;
        ForgetDate=DateTime.UtcNow.AddMinutes(forgetMinute);
        RaiseDomainEvent(new SendEmailDomainEvent(Email!,$"your reset code is {code} and it will after ${forgetMinute} minutes Expired."));
        return Result.Success<bool>();
        
    }

    private List<IDomainEvent> _domainEvents { get; } = new List<IDomainEvent>();
    public List<IDomainEvent> GetDomainEvents() => _domainEvents.ToList();

    public IDomainEvent[] ClearDomainEvents()
    {
        
        IDomainEvent[] dequeuedEvents = _domainEvents.ToArray();
        _domainEvents.Clear();
        return dequeuedEvents;
    }

    public void RaiseDomainEvent(IDomainEvent @event)
    {
        _domainEvents.Add(@event);
        
        
    }
}