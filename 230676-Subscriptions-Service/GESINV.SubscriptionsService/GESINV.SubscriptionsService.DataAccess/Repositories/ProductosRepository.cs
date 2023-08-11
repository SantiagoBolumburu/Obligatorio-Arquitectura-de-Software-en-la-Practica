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
    public class ProductosRepository : IProductosRepository
    {
        private readonly DbSet<Producto> _productos;
        private readonly DbContext _context;

        public ProductosRepository(DbContext context)
        {
            _context = context;
            _productos = context.Set<Producto>();
        }

        public Producto Crear(Producto producto)
        {
            try
            {
                _productos.Add(producto);
                _context.SaveChanges();

                return producto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Producto? ObtenerPorMainId(Guid productoId)
        {
            try
            {
                Producto? producto = _productos.FirstOrDefault(p => p.ProductoMainId == productoId);

                return producto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
