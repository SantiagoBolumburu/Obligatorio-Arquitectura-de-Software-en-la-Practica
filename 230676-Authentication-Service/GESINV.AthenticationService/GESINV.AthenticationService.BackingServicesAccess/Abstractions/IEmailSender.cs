using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.AthenticationService.BackingServicesAccess.Abstractions
{
    public interface IEmailSender
    {
        void EnviarCorreo(string fromEmail, string toEmail, string subject, string body);
        bool GetHealth();
    }
}
