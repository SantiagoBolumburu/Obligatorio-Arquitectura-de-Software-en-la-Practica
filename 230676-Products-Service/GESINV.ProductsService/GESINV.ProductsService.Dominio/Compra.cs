using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.Dominio
{
    public class Compra
    {
        public Guid Id { get; set; }
        public DateTime FechaCompra { get; set; }
        public decimal CostoTotalEnPesos { get; set; }
        public List<DetalleCompraProducto> DetallesComprasProductos { get; set; }
        public Guid EmpresaId { get; set; }
        public Proveedor Proveedor { get; set; }
        public Guid ProveedorId { get; set; }
    }
}
