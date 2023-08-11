using GESINV.AthenticationService.BackingServicesAccess.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.AthenticationService.BackingServicesAccess
{
    public class EmailSenderForDevelopment : IEmailSender
    {
        public void EnviarCorreo(string fromEmail, string toEmail, string subject, string body)
        {
            Console.WriteLine("FromEmail: " + fromEmail);
            Console.WriteLine("ToEmail:   " + toEmail);
            Console.WriteLine("Subject:   " + subject);
            Console.WriteLine("Body:      " + body);
        }

        public bool GetHealth()
        {
            return true;
        }
    }
}
