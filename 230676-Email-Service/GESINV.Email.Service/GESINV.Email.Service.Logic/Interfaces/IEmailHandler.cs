using GESINV.Email.Service.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.Email.Service.Logic.Interfaces
{
    public interface IEmailHandler
    {
        public void SendEmail(string fromMail, string toMail, string subject, string body);
    }
}
