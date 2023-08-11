using GESINV.AthenticationService.PersistanceAccess.Interface;
using GESINV.AthenticationService.Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.AthenticationService.PersistanceAccess.Repositories
{
    public class EmpresasRepository : IEmpresasRepository
    {
        private readonly DbSet<Empresa> _empresas;
        private readonly DbContext _context;

        public EmpresasRepository(DbContext context)
        {
            _context = context;
            _empresas = context.Set<Empresa>();
        }

        public Empresa Add(Empresa empresa)
        {
            try
            {
                _empresas.Add(empresa);
                _context.SaveChanges();

                return empresa;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public bool ExistByName(string name)
        {
            return _empresas.Any(u => u.Nombre == name);
        }
    }
}
