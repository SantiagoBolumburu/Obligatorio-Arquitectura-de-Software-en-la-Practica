using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.Models
{
    public class ProductoModelOut
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ImagenPath { get; set; }
        public decimal Precio { get; set; }
        public int CantidadEnInventarioInicial { get; set; }
        public int CantidadComprada { get; set; }
        public int CantidadVendida { get; set; }
        public int CantidadEnInventario { get; set; }
    }
}
