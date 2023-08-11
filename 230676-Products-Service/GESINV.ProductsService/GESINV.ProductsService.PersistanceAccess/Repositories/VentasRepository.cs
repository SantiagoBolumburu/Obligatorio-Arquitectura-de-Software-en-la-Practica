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
    public class VentasRepository : IVentasRepository
    {
        private readonly DbSet<Venta> _ventas;
        private readonly DbContext _context;

        public VentasRepository(DbContext context)
        {
            _context = context;
            _ventas = context.Set<Venta>();
        }

        public Venta Crear(Venta venta)
        {
            try
            {
                if (venta is null)
                    throw new ArgumentNullException("No se puede crar una venta nula.");

                _ventas.Add(venta);
                _context.SaveChanges();

                return venta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public Venta Actualizar(Venta venta)
        {
            try
            {
                if (venta is null)
                    throw new ArgumentNullException("No se puede crar una venta nula.");

                _ventas.Update(venta);
                _context.SaveChanges();

                return venta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Venta> ObtenerPorFechaHastaInclusive(DateTime dateTime, bool programada, bool realizada)
        {
            try
            {
                List<Venta> ventas = _ventas.Where(v => v.FechaVenta <= dateTime 
                    && v.Programada == programada 
                    && v.Realizada == realizada)
                    .Include(v => v.DetallesVentasProductos)
                    .ThenInclude(d => d.Producto)
                    .ToList();

                return ventas;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public Venta? ObtenerPorIdYEmpresaId(Guid ventaId, Guid empresaId)
        {
            try
            {
                Venta? venta = _ventas.Include(v => v.DetallesVentasProductos)
                    .ThenInclude(d => d.Producto)
                    .FirstOrDefault(v => v.Id == ventaId && v.EmpresaId == empresaId);

                return venta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Venta> ObtenerPorEmpresaIdPaginado(Guid empresaId, DateTime fechaDesde,
            DateTime fechaHasta, int indicePagina, int cantidadPorPagina, bool realizada)
        {
            try
            {
                List<Venta> ventas = _ventas.Include(v => v.DetallesVentasProductos)
                    .ThenInclude(d => d.Producto)
                    .Where(v => v.EmpresaId == empresaId)
                    .Where(v => v.FechaVenta >= fechaDesde && v.FechaVenta < fechaHasta)
                    .Where(v => v.Realizada == realizada)
                    .Skip(cantidadPorPagina * indicePagina)
                    .Take(cantidadPorPagina).ToList();

                return ventas;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Venta> ObtenerPorEmpresaId(Guid empresaId, bool programadas)
        {
            try
            {
                List<Venta> ventas = _ventas.Include(v => v.DetallesVentasProductos)
                    .ThenInclude(d => d.Producto)
                    .Where(v => v.EmpresaId == empresaId)
                    .Where(v => v.Programada == programadas)
                    .ToList();

                return ventas;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
