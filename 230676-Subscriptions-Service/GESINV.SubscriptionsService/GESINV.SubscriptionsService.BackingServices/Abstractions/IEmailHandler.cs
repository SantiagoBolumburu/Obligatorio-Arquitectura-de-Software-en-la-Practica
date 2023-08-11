using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.SubscriptionsService.BackingServices.Abstractions
{
    public interface IEmailHandler
    {
        void EnviarCorreo(string fromEmail, string toEmail, string subject, string body);
        bool GetHealth();
    }
}
