using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.Dominio
{
    public class DetalleCompraProducto
    {
        public Guid Id { get; set; }
        public int Cantidad { get; set; }
        public Producto Producto { get; set; }
        public Guid ProductoId { get; set; }
        public Compra Compra { get; set; }
        public Guid CompraId { get; set; }
        public int StockDespuesDeCompra { get; set; }
    }
}
