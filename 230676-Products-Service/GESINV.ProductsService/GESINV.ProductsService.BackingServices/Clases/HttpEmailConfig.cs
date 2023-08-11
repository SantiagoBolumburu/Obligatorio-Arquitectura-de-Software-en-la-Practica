using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.BackingServices.Clases
{
    public class HttpEmailConfig
    {
        public string? FromMail { get; set; }
        public string? ToMail { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
    }
}
