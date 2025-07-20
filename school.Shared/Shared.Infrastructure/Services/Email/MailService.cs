using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Shared.Domain.Services.Email;

namespace Shared.Infrastructure.Services.Email;

public class MailService:IMailService
{
    private readonly MailSetting _mailSetting;

    public MailService(IOptions<MailSetting> mailSetting) {


        this._mailSetting = mailSetting.Value;
    }


    public bool SendMail(string email, string subject, string message)
    {
        
        
        try
        {

            using MimeMessage emailMessage = new MimeMessage();
            emailMessage.From.Add(MailboxAddress.Parse(_mailSetting.From));
            emailMessage.To.Add(MailboxAddress.Parse(email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(TextFormat.Text)
            {
                Text=message

            };
            
            using SmtpClient mailClient = new SmtpClient();

            mailClient.Connect(_mailSetting.SmtpServer,_mailSetting.Port,MailKit.Security.SecureSocketOptions.StartTls);
            mailClient.Authenticate(_mailSetting.Username,_mailSetting.Password); 
            mailClient.Send(emailMessage);
            mailClient.Disconnect(true);
            return true;
        }
        catch (Exception ex)
        {

            return false;
        }




    }
}