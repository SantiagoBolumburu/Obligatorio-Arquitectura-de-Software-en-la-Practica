using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.BackingServices.Interface.Clases
{
    public class ProductoEventInfo
    {
        public static string TipoCompra = "compra";
        public static string TipoVenta = "venta";
        public static string TipoCambioStock = "cambiostock";

        public string TipoEvento { get; set; }
        public Guid ProductoId { get; set; }
        public Guid? EntidadId { get; set; }
        public string ProductoNombre { get; set; }
        public int StockActual { get; set; }
        public string Descripcion { get; set; }
    }
}
