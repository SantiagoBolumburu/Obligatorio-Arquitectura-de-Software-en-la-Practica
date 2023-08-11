using GESINV.AthenticationService.Dominio;
using GESINV.AthenticationService.PersistanceAccess.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.AthenticationService.PersistanceAccess.Repositories
{
    public class AppkeyRepository : IAppkeyRepository
    {
        private readonly DbSet<ApplicationKey> _applicationKeys;
        private readonly DbContext _context;

        public AppkeyRepository(DbContext context)
        {
            _context = context;
            _applicationKeys = context.Set<ApplicationKey>();
        }

        public ApplicationKey Create(ApplicationKey applicationKey)
        {
            try
            {
                _applicationKeys.Add(applicationKey);
                _context.SaveChanges();

                return applicationKey;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ApplicationKey? GetActiveByUserId(Guid id)
        {
            try
            {
                ApplicationKey? applicationKey = _applicationKeys.FirstOrDefault(a => a.UsuarioId == id && a.Activa);

                return applicationKey;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<ApplicationKey> GetAllActivesByUserId(Guid id)
        {
            try
            {
                List<ApplicationKey> applicationKeys = _applicationKeys.Where(a => a.UsuarioId == id && a.Activa).ToList();

                return applicationKeys;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ApplicationKey Update(ApplicationKey applicationKey)
        {
            try
            {
                _applicationKeys.Update(applicationKey);
                _context.SaveChanges();

                return applicationKey;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
