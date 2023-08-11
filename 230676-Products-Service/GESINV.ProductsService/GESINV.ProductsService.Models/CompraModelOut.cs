using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.Models
{
    public class CompraModelOut
    {
        public Guid Id { get; set; }
        public Guid ProveedorId { get; set; }
        public string NombreProveedor { get; set; }
        public DateTime FechaCompra { get; set; }
        public decimal CostoTotal { get; set; }
        public List<Tuple<Guid, string, int>> ProductosNombreYCantidad { get; set; }
    }
}
