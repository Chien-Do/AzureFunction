using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace FunctionAppDemo
{
    public class EmailLicenseFile
    {
        [FunctionName("EmailLicenseFile")]
        public void Run([BlobTrigger("licenses/{orderId}.lic",
            Connection = "AzureWebJobsStorage")]string licenseFileContents,
            [SendGrid(ApiKey = "SendGridAPIKey")] ICollector<SendGridMessage> sender,
            [Table("orders", "orders","{orderId}")] Order order,
            string orderId,
            ILogger log)
        {
            var email = order.Email ;
            //var message = new SendGridMessage();
            //message.From = new EmailAddress(Environment.GetEnvironmentVariable("EmailSender"));
            //message.AddTo(email);

            //var plainText = System.Text.Encoding.UTF8.GetBytes(licenseFileContents);
            //var base64 = Convert.ToBase64String(plainText);
            //message.AddAttachment(orderId, base64, "text/plain");
            //message.Subject = "Your License File";
            //message.HtmlContent = "Thank you for y our order";
            //if (!email.EndsWith("@test.com"))
            //{
            //    sender.Add(message);
            //}
            //var client = new SendGridClient(Environment.GetEnvironmentVariable("SendGridAPIKey"));

        }
    }
}
