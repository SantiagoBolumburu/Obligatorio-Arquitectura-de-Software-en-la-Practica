using GESINV.ProductsService.Dominio;
using GESINV.ProductsService.PersistanceAccess.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.PersistanceAccess.Repositories
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

        public Producto Create(Producto producto)
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

        public List<Producto> GetByEmpresaId_ProductosAccesibles(Guid empresaId)
        {
            try
            {
                List<Producto> productos = _productos.Where(p => p.EmpresaId == empresaId && p.Accesible).ToList();

                return productos;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Producto> GetByEmpresaId_ProductosMasVendidos(Guid empresaId, int cantidad)
        {
            try
            {
                List<Producto> productos = _productos.Where(p => p.EmpresaId == empresaId)
                    .OrderByDescending(p => p.CantidadVendida)
                    .Take(cantidad).ToList();

                return productos;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Producto? GeyById_AndEmpresaId_ProductoAccesible(Guid productoId, Guid empresaId)
        {
            try
            {
                Producto? producto = _productos.FirstOrDefault(p => p.Id == productoId && p.EmpresaId == empresaId && p.Accesible);

                return producto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Producto Update(Producto producto)
        {
            try
            {
                _productos.Update(producto);
                _context.SaveChanges();

                return producto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdateCantidadCompras(Dictionary<Guid, int> paresIdCantidad)
        {
            try
            {
                List<Producto> productosComprados = _productos.ToList();

                productosComprados.ForEach(p => p.CantidadComprada += paresIdCantidad.GetValueOrDefault(p.Id));

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdateCantidadVentas(Dictionary<Guid, int> paresIdCantidad)
        {
            try
            {
                List<Producto> productosVendidos = _productos.ToList();

                productosVendidos.ForEach(p => p.CantidadVendida += paresIdCantidad.GetValueOrDefault(p.Id));

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
