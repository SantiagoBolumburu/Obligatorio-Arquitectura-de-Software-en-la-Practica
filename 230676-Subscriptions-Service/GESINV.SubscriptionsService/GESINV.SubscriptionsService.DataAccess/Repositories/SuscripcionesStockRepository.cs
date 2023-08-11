using GESINV.SubscriptionsService.DataAccess.Interface;
using GESINV.SubscriptionsService.Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.SubscriptionsService.DataAccess.Repositories
{
    public class SuscripcionesStockRepository : ISuscripcionesStockRepository
    {
        private readonly DbSet<StockSubscription> _stockSubscription;
        private readonly DbContext _context;

        public SuscripcionesStockRepository(DbContext context)
        {
            _context = context;
            _stockSubscription = context.Set<StockSubscription>();
        }

        public StockSubscription Crear(StockSubscription compraVentaSubscription)
        {
            try
            {
                _stockSubscription.Add(compraVentaSubscription);
                _context.SaveChanges();

                return compraVentaSubscription;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Eliminar(StockSubscription compraVentaSubscription)
        {
            try
            {
                _stockSubscription.Remove(compraVentaSubscription);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public StockSubscription? ObtenerPorUsuarioIdYProductoId(Guid productoId, Guid usuarioId)
        {
            try
            {
                StockSubscription? subscription = _stockSubscription.Include(s => s.Producto)
                    .FirstOrDefault(s => s.Producto.ProductoMainId == productoId && s.UsuarioId == usuarioId);

                return subscription;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<StockSubscription> ObtenerPorProductoMainId(Guid ProductoMainId)
        {
            try
            {
                List<StockSubscription> subscriptions = _stockSubscription
                    .Include(s => s.Producto)
                    .Where(s => s.Producto.ProductoMainId == ProductoMainId).ToList();

                return subscriptions;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<StockSubscription> ObtenerPorUsuarioId(Guid usuarioId)
        {
            try
            {
                List<StockSubscription> subscriptions = _stockSubscription
                    .Include(s => s.Producto)
                    .Where(s => s.UsuarioId == usuarioId).ToList();

                return subscriptions;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
