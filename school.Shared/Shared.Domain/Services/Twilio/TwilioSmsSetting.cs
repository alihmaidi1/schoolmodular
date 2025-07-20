using System.ComponentModel.DataAnnotations;

namespace Shared.Domain.Services.Twilio;

public class TwilioSmsSetting
{
    [Required]
    public string AccountSID{get;set;}

    [Required]
    
    public string AuthToken{get;set;}

    [Required]
    
    public string TwilioPhoneNumber{get;set;}

    
}
