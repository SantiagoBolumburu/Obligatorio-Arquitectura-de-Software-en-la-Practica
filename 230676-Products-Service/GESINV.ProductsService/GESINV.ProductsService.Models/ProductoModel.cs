using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.Models
{
    public class ProductoModel
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ImagenPath { get; set; }
        public decimal Precio { get; set; }
        public int CantidadEnInventario { get; set; }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Descripcion) || string.IsNullOrEmpty(ImagenPath))
                throw new ArgumentException("Los campos 'Nombre', 'Descripcion' y 'Imagen' son obligatorios.");

            if (Precio < 0) throw new ArgumentException("Los productos no pueden tener precion negativo (menos a 0).");

            if (CantidadEnInventario < 0) throw new ArgumentException("La cantidad en el inventario de un producto no puede ser menor a 0.");
        }
    }
}
