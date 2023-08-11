using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.Dominio
{
    public class Venta
    {
        public Guid Id { get; set; }
        public string NombreCliente { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal MontoTotalEnPesos { get; set; }
        public bool Programada { get; set; }
        public bool Realizada { get; set; }
        public Guid EmpresaId { get; set; }
        public List<DetalleVentaProducto> DetallesVentasProductos { get; set; }
    }
}
