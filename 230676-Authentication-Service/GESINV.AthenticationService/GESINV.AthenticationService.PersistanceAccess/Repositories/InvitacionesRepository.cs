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
    public class InvitacionesRepository : IInvitacionesRepository
    {
        private readonly DbSet<Invitacion> _invitaciones;
        private readonly DbContext _context;

        public InvitacionesRepository(DbContext context)
        {
            _context = context;
            _invitaciones = context.Set<Invitacion>();
        }

        public Invitacion Create(Invitacion invitacion)
        {
            try
            {
                _invitaciones.Add(invitacion);
                _context.SaveChanges(); 

                return invitacion;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public Invitacion? GetById(Guid id)
        {
            try
            {
                Invitacion? invitacion = _invitaciones.Include(i => i.Empresa).Include(i => i.Invitador).FirstOrDefault(i => i.Id == id);

                return invitacion;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public Invitacion Update(Invitacion invitacion)
        {
            try
            {
                _invitaciones.Update(invitacion);
                _context.SaveChanges();

                return invitacion;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
