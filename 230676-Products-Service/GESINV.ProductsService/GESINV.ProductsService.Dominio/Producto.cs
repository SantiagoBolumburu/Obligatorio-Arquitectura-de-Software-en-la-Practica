using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.Dominio
{
    public class Producto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ImagenPath { get; set; }
        public decimal Precio { get; set; }
        public int CantidadEnInventarioInicial { get; set; }
        public bool Accesible { get; set; }
        public int CantidadVendida { get; set; }
        public int CantidadComprada { get; set; }
        public Guid EmpresaId { get; set; }
        public List<DetalleCompraProducto> DetallesComprasProductos { get; set; }
        public List<DetalleVentaProducto> DetallesVentasProductos { get; set; }

        public int CantidadEnInventario()
        {
            return CantidadEnInventarioInicial + CantidadComprada - CantidadVendida;
        }
    }
}
