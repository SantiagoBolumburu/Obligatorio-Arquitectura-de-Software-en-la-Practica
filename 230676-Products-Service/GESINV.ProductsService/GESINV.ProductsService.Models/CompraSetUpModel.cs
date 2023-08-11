using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.Models
{
    public class CompraSetUpModel
    {
        public List<ProductoModelOut> Productos { get; set; }
        public List<ProveedorModelOut> Proveedores { get; set; }
    }
}
