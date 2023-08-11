using GESINV.ProductsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.Logic.Interface
{
    public interface IProductosLogic
    {
        ProductoModelOut CrearProducto(ProductoModel productoModel);
        List<ProductoModelOut> ObtenerTodosLosProductosDeLaEmpresaDeUsuarioLoggeado();
        ProductoModelOut? ObtenerProductoDeLaEmpresaDeUsuarioLoggeado(Guid productoId);
        ProductoModelOut ModificarProducto(Guid productoId, ProductoModel productoModel);
        void EliminarProducto(Guid productoId);
        List<ProductoModelOut> ObtenerProductosMasVendidos(int? cantidad);
        void EmitirTodosLosPorductos();
    }
}
