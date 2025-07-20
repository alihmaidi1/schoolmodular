using Twilio.Rest.Api.V2010.Account;

namespace Shared.Domain.Services.Twilio;

public interface ISmsTwilioService
{

    public Task<MessageResource> Send(string mobileNumber,string Body);
    
    
    
}
