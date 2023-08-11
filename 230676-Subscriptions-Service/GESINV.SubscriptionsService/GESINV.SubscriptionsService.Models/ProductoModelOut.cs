using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.SubscriptionsService.Models
{
    public class ProductoModelOut
    {
        public Guid Id { get; set; }
        public Guid ProductoId { get; set; }
        public Guid EmpresaId { get; set; }
    }
}
