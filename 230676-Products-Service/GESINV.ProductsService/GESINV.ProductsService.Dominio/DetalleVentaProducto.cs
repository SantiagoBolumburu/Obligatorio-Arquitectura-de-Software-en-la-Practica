using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.Dominio
{
    public class DetalleVentaProducto
    {
        public Guid Id { get; set; }
        public int Cantidad { get; set; }
        public Producto Producto { get; set; }
        public Guid ProductoId { get; set; }
        public Venta Venta { get; set; }
        public Guid VentaId { get; set; }
        public int StockDespuesDeVenta { get; set; }
    }
}
