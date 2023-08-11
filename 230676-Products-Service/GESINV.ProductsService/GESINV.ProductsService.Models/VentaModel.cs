using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.Models
{
    public class VentaModel
    {
        public string NombreCliente { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal MontoTotalEnPesos { get; set; }
        public List<Tuple<Guid, int>> ProductosYCantidad { get; set; }

        public void Validar()
        {
            if (string.IsNullOrEmpty(NombreCliente))
                throw new ArgumentNullException("El parametro ''Nombre Cliente' es obligatorio.");

            if (MontoTotalEnPesos < 0)
                throw new ArgumentException("El Monto Total no puede ser menor a 0.");

            if (ProductosYCantidad.Count < 1)
                throw new ArgumentException("Toda venta debe incluir al menos un producto.");

            if (ProductosYCantidad.Any(p => p.Item2 < 1))
                throw new ArgumentException("La minima cantidad para un determinado producto en la venta es 1.");
        }
    }
}
