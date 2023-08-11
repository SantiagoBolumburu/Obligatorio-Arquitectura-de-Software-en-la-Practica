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
    public class SubscriptionsCompraVentaRepository : ISubscriptionsCompraVentaRepository
    {
        private readonly DbSet<CompraVentaSubscription> _compraVentaSubscription;
        private readonly DbContext _context;

        public SubscriptionsCompraVentaRepository(DbContext context)
        {
            _context = context;
            _compraVentaSubscription = context.Set<CompraVentaSubscription>();
        }

        public CompraVentaSubscription Crear(CompraVentaSubscription compraVentaSubscription)
        {
            try
            {
                _compraVentaSubscription.Add(compraVentaSubscription);
                _context.SaveChanges();

                return compraVentaSubscription;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Eliminar(CompraVentaSubscription compraVentaSubscription)
        {
            try
            {
                _compraVentaSubscription.Remove(compraVentaSubscription);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public CompraVentaSubscription? ObtenerPorUsuarioIdYProductoId(Guid productoId, Guid usuarioId)
        {
            try
            {
                CompraVentaSubscription? subscription = _compraVentaSubscription
                    .Include(s => s.Producto)
                    .FirstOrDefault(s => s.Producto.ProductoMainId == productoId && s.UsuarioId == usuarioId);

                return subscription;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<CompraVentaSubscription> ObtenerPorProductoMainId(Guid ProductoMainId)
        {
            try
            {
                List<CompraVentaSubscription> subscriptions = _compraVentaSubscription
                    .Include(s => s.Producto)
                    .Where(s => s.Producto.ProductoMainId == ProductoMainId).ToList();


                return subscriptions;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<CompraVentaSubscription> ObtenerPorUsuarioId(Guid usuarioId)
        {
            try
            {
                List<CompraVentaSubscription> subscriptions = _compraVentaSubscription
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
