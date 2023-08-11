using GESINV.SubscriptionsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.SubscriptionsService.Logic.Interface
{
    public interface ISuscripcionesCompraVentaLogic
    {
        SubscriptionModelOut Crear(Guid productoId);
        void Eliminar(Guid productoId);
        List<SubscriptionModelOut> ObtenerPorUsuarioEnSession();
    }
}
