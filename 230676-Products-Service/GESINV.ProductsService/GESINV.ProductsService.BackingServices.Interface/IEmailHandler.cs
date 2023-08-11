using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.BackingServices.Interface
{
    public interface IEmailHandler
    {
        void SendEmail(string? fromMail, string toMail, string subject, string body);
        bool GetHealth();
    }
}
