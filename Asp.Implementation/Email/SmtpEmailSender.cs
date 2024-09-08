using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Asp.Application.Emails;

namespace Asp.Implementation.Email
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly string _email;
        private readonly string _password;

        public SmtpEmailSender(string email, string password)
        {
            _email = email;
            _password = password;
        }

        public void Send(EmailMessage mail)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(_email, _password)
            };

            var message = new MailMessage(_email, mail.To);
            message.Subject = mail.Title;
            message.Body = mail.Body;
            message.IsBodyHtml = true;

            smtp.Send(message);
        }
    }
}
