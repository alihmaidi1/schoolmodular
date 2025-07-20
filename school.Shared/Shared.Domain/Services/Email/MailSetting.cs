using System.ComponentModel.DataAnnotations;

namespace Shared.Domain.Services.Email;

public class MailSetting
{
    [Required]
    public string From { get; init; }
    [Required]

    public string SmtpServer { get; init; }      

    [Required]
    
    public int Port { get; init; }               
    [Required]
    
    public string Username { get; init; }
    [Required]
    
    public string Password { get; init; }

    
}