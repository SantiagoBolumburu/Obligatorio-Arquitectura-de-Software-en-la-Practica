using GESINV.SubscriptionsService.BackingServices.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.SubscriptionsService.BackingServices
{
    public class EmailHandlerForDevelopment : IEmailHandler
    {
        public void EnviarCorreo(string fromEmail, string toEmail, string subject, string body)
        {
            Console.WriteLine("FromEmail: " + fromEmail);
            Console.WriteLine("ToEmail:   " + toEmail);
            Console.WriteLine("Subject:   " + subject);
            Console.WriteLine("Body:      " + body);
            Console.WriteLine();
        }

        public bool GetHealth()
        {
            return true;
        }
    }
}
