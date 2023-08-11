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
    public class ProveedoresRepository : IProveedoresRepository
    {
        private readonly DbSet<Proveedor> _proveedor;
        private readonly DbContext _context;

        public ProveedoresRepository(DbContext context)
        {
            _context = context;
            _proveedor = context.Set<Proveedor>();
        }

        public Proveedor Crear(Proveedor proveedor)
        {
            try
            {
                _proveedor.Add(proveedor);
                _context.SaveChanges();

                return proveedor;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Proveedor Modificar(Proveedor proveedor)
        {
            try
            {
                _proveedor.Update(proveedor);
                _context.SaveChanges();

                return proveedor;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Proveedor> ObtenerPorEmpresaIdProveedoresAccesibles(Guid empresaId)
        {
            try
            {
                List<Proveedor> proveedores = _proveedor.Where(p => p.EmpresaId == empresaId && p.Accesible).ToList();

                return proveedores;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Proveedor? ObtenerPorIdYEmpresaIdProveedorAccesible(Guid proveedorId, Guid empresaId)
        {
            try
            {
                Proveedor? proveedor = _proveedor.FirstOrDefault(p => p.Id == proveedorId && p.EmpresaId == empresaId && p.Accesible);

                return proveedor;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
