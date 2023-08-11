using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.SubscriptionsService.Dominio
{
    public class Producto
    {
        public Guid Id { get; set; }
        public Guid ProductoMainId { get; set; }
        public Guid EmpresaId { get; set; }
        public List<CompraVentaSubscription> CompraVentasSubscriptions { get; set; }
        public List<StockSubscription> StockSubscriptions { get; set; }
    }
}
