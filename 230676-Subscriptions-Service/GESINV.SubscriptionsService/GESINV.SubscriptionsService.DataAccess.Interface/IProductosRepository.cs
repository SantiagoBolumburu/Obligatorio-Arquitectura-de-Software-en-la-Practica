using GESINV.SubscriptionsService.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.SubscriptionsService.DataAccess.Interface
{
    public interface IProductosRepository
    {
        Producto Crear(Producto producto);
        Producto? ObtenerPorMainId(Guid productoId);
    }
}
