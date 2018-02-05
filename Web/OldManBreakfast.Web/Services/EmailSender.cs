using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace OldManBreakfast.Web.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var client = new SmtpClient("localhost");
                //client.UseDefaultCredentials = false;
                //client.Credentials = new NetworkCredential("username", "password");

                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("web@old-man-breakfast.com");
                mailMessage.To.Add(email);
                mailMessage.Body = message;
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = subject;
                client.Send(mailMessage);
            }
            catch { }

            return Task.CompletedTask;
        }
    }
}
