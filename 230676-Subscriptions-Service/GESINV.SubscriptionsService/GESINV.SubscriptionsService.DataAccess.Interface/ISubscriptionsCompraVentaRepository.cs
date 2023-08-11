using GESINV.SubscriptionsService.DataAccess.Interface;
using GESINV.SubscriptionsService.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.SubscriptionsService.DataAccess.Interface
{
    public interface ISubscriptionsCompraVentaRepository
    {
        CompraVentaSubscription Crear(CompraVentaSubscription compraVentaSubscription);
        CompraVentaSubscription? ObtenerPorUsuarioIdYProductoId(Guid productoId, Guid usuarioId);
        void Eliminar(CompraVentaSubscription compraVentaSubscription);
        List<CompraVentaSubscription> ObtenerPorProductoMainId(Guid ProductoMainId);
        List<CompraVentaSubscription> ObtenerPorUsuarioId(Guid usuarioId);
    }
}
