using GESINV.ProductsService.PersistanceAccess.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.PersistanceAccess
{
    public class PersistanceConnectionChecker : IPersistanceConnectionChecker
    {
        private readonly DbContext _context;

        public PersistanceConnectionChecker(DbContext context)
        {
            _context = context;
        }

        public bool CanConnect()
        {
            try
            {
                _context.Database.OpenConnection();
                _context.Database.CloseConnection();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
