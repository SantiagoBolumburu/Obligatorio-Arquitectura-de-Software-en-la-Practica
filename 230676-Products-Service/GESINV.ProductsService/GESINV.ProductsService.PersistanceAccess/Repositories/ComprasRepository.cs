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
    public class ComprasRepository : IComprasRepository
    {
        private readonly DbSet<Compra> _compras;
        private readonly DbContext _context;

        public ComprasRepository(DbContext context)
        {
            _context = context;
            _compras = context.Set<Compra>();
        }


        public Compra Crear(Compra compra)
        {
            try
            {
                _compras.Add(compra);
                _context.SaveChanges();

                return compra;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Compra? ObtenerPorIdYEmpresaId(Guid compraId, Guid empresaId)
        {
            try
            {
                Compra? compra = _compras.Include(c => c.Proveedor)
                    .Include(c => c.DetallesComprasProductos)
                    .ThenInclude(d => d.Producto)
                    .FirstOrDefault(c => c.Id == compraId && c.EmpresaId == empresaId);

                return compra;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Compra> ObtenerPorEmpresaId(Guid empresaId)
        {
            try
            {
                List<Compra> compras = _compras.Include(c => c.Proveedor)
                    .Include(c => c.DetallesComprasProductos)
                    .ThenInclude(d => d.Producto)
                    .Where(c => c.EmpresaId == empresaId).ToList();

                return compras;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Compra> ObtenerPorEmpresaIdNombreProveedorYFecha(Guid empresaId, Guid proveedorId, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            try
            {
                List<Compra> compras = _compras.Include(c => c.Proveedor)
                    .Include(c => c.DetallesComprasProductos)
                    .ThenInclude(d => d.Producto)
                    .Where(c => c.EmpresaId == empresaId
                        && c.Proveedor.Id == proveedorId
                        && c.FechaCompra >= fechaDesde
                        && c.FechaCompra < fechaHasta).ToList();

                return compras;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
