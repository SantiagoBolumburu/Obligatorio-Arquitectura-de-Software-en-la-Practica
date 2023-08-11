using GESINV.ProductsService.BackingServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.BackingServices
{
    public class EmailHandlerForTesting : IEmailHandler
    {
        public bool GetHealth()
        {
            return true;
        }

        public void SendEmail(string? fromMail, string toMail, string subject, string body)
        {
            string fromMailConcrete = fromMail ?? "default@email.com";

            Console.WriteLine("FromMail: " + fromMailConcrete);
            Console.WriteLine("ToMail:   " + toMail);
            Console.WriteLine("Subject:  " + subject);
            Console.WriteLine("Body:     " + body);
        }
    }
}
