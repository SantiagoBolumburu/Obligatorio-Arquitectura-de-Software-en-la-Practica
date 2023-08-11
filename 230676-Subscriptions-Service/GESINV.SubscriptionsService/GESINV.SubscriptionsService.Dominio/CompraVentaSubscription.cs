using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.SubscriptionsService.Dominio
{
    public class CompraVentaSubscription
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public string Email { get; set; }
        public Guid ProductoId { get; set; }
        public Producto Producto { get; set; }
    }
}
