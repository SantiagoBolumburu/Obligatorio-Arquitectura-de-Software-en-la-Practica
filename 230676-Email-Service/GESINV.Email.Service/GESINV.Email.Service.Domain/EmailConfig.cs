using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.Email.Service.Domain
{
    public class EmailConfig
    {
        //string fromMail, string toMail, string subject, string body
        public string? FromMail { get; set; }
        public string? ToMail { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
    }
}
