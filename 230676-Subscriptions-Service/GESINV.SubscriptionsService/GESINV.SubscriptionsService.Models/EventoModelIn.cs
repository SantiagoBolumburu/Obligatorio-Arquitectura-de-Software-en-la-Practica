using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.SubscriptionsService.Models
{
    public class EventoModelIn
    {
        public const string TipoCompra = "compra";
        public const string TipoVenta = "venta";
        public const string TipoCambioStock = "cambiostock";

        public string TipoEvento { get; set; }
        public Guid ProductoId { get; set; }
        public Guid? EntidadId { get; set; }
        public string ProductoNombre { get; set; }
        public int StockActual { get; set; }
        public string? Descripcion { get; set; }
    }
}
