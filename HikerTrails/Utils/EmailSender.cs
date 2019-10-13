using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace HikerTrails.Utils
{
    public class EmailSender
    {
        // Please use your API KEY here.
        private const String API_KEY = "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";

        public void Send(String toEmail, String subject, String contents, String imagePath)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("noreply@localhost.com", "HikerTrails Admin");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            byte[] imageArr = File.ReadAllBytes(imagePath);
            string imageBase64 = Convert.ToBase64String(imageArr);
            msg.AddAttachment("Hike_Image.jpg", imageBase64);
            var response = client.SendEmailAsync(msg);
        }
    }
}