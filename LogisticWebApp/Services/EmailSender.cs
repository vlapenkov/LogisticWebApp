using LogisticWebApp.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Logistic.Web.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {

        private readonly IConfiguration _configuration;
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            using (var client = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = _configuration["Email:Email"],
                    Password = _configuration["Email:Password"]
                };

                client.Credentials = credential;
                client.Host = _configuration["Email:Host"];
                client.Port = int.Parse(_configuration["Email:Port"]);
                client.EnableSsl = true;
                try
                {
                    using (var emailMessage = new MailMessage())
                    {
                        emailMessage.To.Add(new MailAddress(email));
                        emailMessage.From = new MailAddress(_configuration["Email:Email"]);
                        emailMessage.Subject = subject;
                        emailMessage.Body = message;
                        emailMessage.IsBodyHtml = true;
                        client.Send(emailMessage);
                    }
                }
                catch (Exception e)
                {
                    await Task.CompletedTask;
                }
            }
            await Task.CompletedTask;
        }
        /*
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        } */
    }
}
