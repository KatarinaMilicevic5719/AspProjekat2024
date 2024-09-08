using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Application.Emails;

namespace Asp.Implementation.Emails
{
    public class FakeEmailSender : IEmailSender
    {
        public void Send(EmailMessage mail)
        {
            Console.WriteLine("Sending email to: " + mail.To);
            Console.WriteLine("Title: " + mail.Title);
            Console.WriteLine("Body: " + mail.Body);
        }
    }
}
