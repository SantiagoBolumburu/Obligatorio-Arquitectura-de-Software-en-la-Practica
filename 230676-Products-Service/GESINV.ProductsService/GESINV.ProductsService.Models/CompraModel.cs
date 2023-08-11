using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.Models
{
    public class CompraModel
    {
        public Guid ProveedorId { get; set; }
        public DateTime FechaCompra { get; set; }
        public decimal CostoTotal { get; set; }
        public List<Tuple<Guid, int>> ProductosYCantidad { get; set; }

        public void Validar()
        {
            if (CostoTotal < 0)
                throw new ArgumentException("El costo total no puede ser menos que 0.");

            if (ProductosYCantidad.Count < 1)
                throw new ArgumentException("Cada compra debe incluir al menos un producto.");

            if (ProductosYCantidad.Any(p => p.Item2 < 1))
                throw new ArgumentException("La minima cantidad para un determinado producto en la compra es 1.");
        }
    }
}
