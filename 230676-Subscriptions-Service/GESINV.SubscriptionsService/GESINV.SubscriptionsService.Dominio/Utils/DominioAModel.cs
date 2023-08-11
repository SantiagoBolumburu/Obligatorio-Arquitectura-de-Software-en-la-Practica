using GESINV.SubscriptionsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.SubscriptionsService.Dominio.Utils
{
    public static class DominioAModel
    {
        public static ProductoModelOut Convertir(Producto producto)
        {
            ProductoModelOut productoModelOut = new ProductoModelOut()
            {
                Id = producto.Id,
                ProductoId = producto.ProductoMainId,
                EmpresaId = producto.EmpresaId,
            };

            return productoModelOut;
        }

        public static SubscriptionModelOut Convertir(CompraVentaSubscription subscription)
        {
            SubscriptionModelOut subscriptionModelOut = new SubscriptionModelOut()
            {
                UsuarioId = subscription.UsuarioId,
                Email = subscription.Email,
                ProductoId = subscription.Producto.ProductoMainId
            };

            return subscriptionModelOut;
        }

        public static SubscriptionModelOut Convertir(StockSubscription subscription)
        {
            SubscriptionModelOut subscriptionModelOut = new SubscriptionModelOut()
            {
                UsuarioId = subscription.UsuarioId,
                Email = subscription.Email,
                ProductoId = subscription.Producto.ProductoMainId
            };

            return subscriptionModelOut;
        }
    }
}
