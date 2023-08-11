using GESINV.SubscriptionsService.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.SubscriptionsService.DataAccess.Interface
{
    public interface ISuscripcionesStockRepository
    {
        StockSubscription Crear(StockSubscription compraVentaSubscription);
        StockSubscription? ObtenerPorUsuarioIdYProductoId(Guid productoId, Guid usuarioId);
        void Eliminar(StockSubscription compraVentaSubscription);
        List<StockSubscription> ObtenerPorProductoMainId(Guid ProductoMainId);
        List<StockSubscription> ObtenerPorUsuarioId(Guid usuarioId);
    }
}
