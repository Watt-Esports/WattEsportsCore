using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace WattEsportsCore.Services
{
    public class EmailSender : IEmailSender
    {
        public SendGridOptions Options { get; set; }

        public EmailSender(IOptions<SendGridOptions> emailOptions)
        {
            Options = emailOptions.Value;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options.SendGridKey/*, subject, message, email*/);
        }

        static async Task Execute(string sendGridKey)
        {
            var apiKey = sendGridKey;
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("test@example.com", "DX Team"),
                Subject = "Sending with Twilio SendGrid is Fun",
                PlainTextContent = "and easy to do anywhere, even with C#",
                HtmlContent = "<strong>and easy to do anywhere, even with C#</strong>"
            };
            msg.AddTo(new EmailAddress("test@example.com", "Test User"));
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);

            //var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            //var client = new SendGridClient(sendGridKey);
            //var from = new EmailAddress("test@example.com", "Example User");
            //var subject = "Sending with SendGrid is Fun";
            //var to = new EmailAddress("michaelgunn88@gmail.com", "Example User");
            //var plainTextContent = "and easy to do anywhere, even with C#";
            //var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            //var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            //var response = await client.SendEmailAsync(msg);
        }

        //private async Task<Response> Execute(string sendGridKey, string subject, string message, string email)
        //{
        //    var client = new SendGridClient(sendGridKey);

        //    //var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
        //    //var client = new SendGridClient(sendGridKey);
        //    //var from = new EmailAddress("test@example.com", "Example User");
        //    ////var subject = "Sending with SendGrid is Fun";
        //    //var to = new EmailAddress("upperdeeside@hotmail.co.uk", "Example User");
        //    //var plainTextContent = "and easy to do anywhere, even with C#";
        //    //var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
        //    //var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        //    //var response = await client.SendEmailAsync(msg);
        //    var msg = new SendGridMessage()
        //    {
        //        From = new EmailAddress("jhshoes@gmail.com", "JHShoes"),
        //        Subject = subject,
        //        PlainTextContent = message,
        //        HtmlContent = message
        //    };
        //    msg.AddTo(new EmailAddress(email));
        //    try
        //    {
        //        var respone =  await client.SendEmailAsync(msg);
        //        return respone;
        //        //await client.SendEmailAsync(msg);
        //        //return client.SendEmailAsync(msg);
        //    }
        //    catch (Exception)
        //    {
        //    }

        //    return null;
        //}
    }
}

