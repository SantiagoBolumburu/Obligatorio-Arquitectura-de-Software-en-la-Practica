using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.SubscriptionsService.Models
{
    public class UsuarioSubscriptionOut
    {
        public List<SubscriptionModelOut> CompraVentaSubscriptions { get; set; }
        public List<SubscriptionModelOut> StockSubscription { get; set; }
    }
}
