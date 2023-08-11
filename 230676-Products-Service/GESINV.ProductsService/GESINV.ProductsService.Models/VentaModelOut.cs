using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.Models
{
    public class VentaModelOut
    {
        public Guid Id { get; set; }
        public string NombreCliente { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal MontoTotalEnPesos { get; set; }
        public bool Programada { get; set; }
        public bool Realizada { get; set; }
        public List<Tuple<Guid, string, int>> ProductosYCantidad { get; set; }
    }
}
