using Identity.Domain.Event;
using Shared.Domain.Entities;
using Shared.Domain.OperationResult;
using Shared.Domain.Services.Hash;

namespace Identity.Domain.Security.Admin;

public class Admin: Aggregate,IAggregate
{

    private Admin()
    {
        
        Id= Guid.NewGuid();
    }
    
    public string Email { get; private set; }

    
    public string Password { get; private set; }
    
    public string? ForgetCode { get; private set; }
    
    public DateTime? ForgetDate { get; private set; }

    public ICollection<Role> Roles { get; private set; } = new List<Role>();
    

    public static Admin Create(string email,string password,Role role,IWordHasherService wordHasherService)
    {
        ArgumentNullException.ThrowIfNull(wordHasherService);
        ArgumentNullException.ThrowIfNull(role);

        var admin= new Admin()
        {
            Email = email,
            Password = wordHasherService.Hash(password),
            
            

        };

        admin.Roles.Add(role);
        return admin;
    }

    public void ClearForgetCode()
    {
        ForgetCode = null;
        ForgetDate = null;
    }



    public TResult<bool> SetForgetCode(string code,int forgetMinute,IWordHasherService wordHasherService)
    {
        if (forgetMinute<=0)
        {
            return Result.InternalError<bool>(Error.Internal("forget minute must be greater than zero"));
        }
        ForgetCode=wordHasherService.Hash(code);
        ForgetDate=DateTime.UtcNow.AddMinutes(forgetMinute);
        RaiseDomainEvent(new SendEmailDomainEvent(Email!,$"your reset code is {code} and it will after ${forgetMinute} minutes Expired."));
        return Result.Success<bool>();
        
    }

}