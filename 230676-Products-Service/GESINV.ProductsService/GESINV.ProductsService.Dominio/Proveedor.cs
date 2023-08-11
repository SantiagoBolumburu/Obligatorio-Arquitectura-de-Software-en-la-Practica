using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.Dominio
{
    public class Proveedor
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public bool Accesible { get; set; }
        public Guid EmpresaId { get; set; }
        public List<Compra> Compras { get; set; }
    }
}
