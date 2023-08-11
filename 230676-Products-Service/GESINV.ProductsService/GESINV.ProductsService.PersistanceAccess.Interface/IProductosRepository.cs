using GESINV.ProductsService.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.PersistanceAccess.Interface
{
    public interface IProductosRepository
    {
        Producto Create(Producto producto);
        Producto? GeyById_AndEmpresaId_ProductoAccesible(Guid productoId, Guid empresaId);
        Producto Update(Producto producto);
        List<Producto> GetByEmpresaId_ProductosAccesibles(Guid empresaId);
        void UpdateCantidadVentas(Dictionary<Guid, int> paresIdCantidad);
        void UpdateCantidadCompras(Dictionary<Guid, int> paresIdCantidad);
        List<Producto> GetByEmpresaId_ProductosMasVendidos(Guid empresaId, int cantidad);
    }
}
