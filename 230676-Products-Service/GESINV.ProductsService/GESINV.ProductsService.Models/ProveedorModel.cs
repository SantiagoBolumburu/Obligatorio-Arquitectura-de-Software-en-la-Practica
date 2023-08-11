using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.Models
{
    public class ProveedorModel
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Nombre))
                throw new ArgumentNullException("El parametro 'Nombre' es obligatorio.");
            if (string.IsNullOrEmpty(Direccion))
                throw new ArgumentNullException("El parametro 'Direccion' es obligatorio.");
            if (string.IsNullOrEmpty(Email))
                throw new ArgumentNullException("El parametro 'Email' es obligatorio.");
            if (string.IsNullOrEmpty(Telefono))
                throw new ArgumentNullException("El parametro 'Telefono' es obligatorio.");
        }
    }
}
