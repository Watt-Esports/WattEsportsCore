using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace WattEsportsCore.Services
{
    public class EmailSender : IEmailSender
    {
        public SendGridOptions SendGridOptions { get; set; }

        public EmailSender(IOptions<SendGridOptions> emailOptions)
        {
            SendGridOptions = emailOptions.Value;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(SendGridOptions.SendGridKey, subject, message, email);
        }

        private Task Execute(string sendGridKey, string subject, string message, string email)
        {
            var client = new SendGridClient(sendGridKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(SendGridOptions.SendGridUser, "WattEsports"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));
            try
            {
                var response = client.SendEmailAsync(msg);
                var test = response.IsCompletedSuccessfully;
                return response;
            }
            catch (Exception)
            {
            }

            return null;
        }

    }
}

