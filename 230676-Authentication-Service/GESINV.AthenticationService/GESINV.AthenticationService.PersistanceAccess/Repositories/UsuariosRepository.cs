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
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly DbSet<Usuario> _usuarios;
        private readonly DbContext _context;

        public UsuariosRepository(DbContext context)
        {
            _context = context;
            _usuarios = context.Set<Usuario>();
        }

        public Usuario Add(Usuario usuario)
        {
            try
            {
                _usuarios.Add(usuario);
                _context.SaveChanges();

                return usuario;
            }
            catch (Exception ex) 
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public bool ExistsByEmail(string email)
        {
            return _usuarios.Any(u => u.Email == email);
        }

        public Usuario? GetByCredentials(string email, string password)
        {
            Usuario? usuario = _usuarios.Include(u => u.Empresa).FirstOrDefault(u => u.Email == email && u.Contrasenia == password);

            return usuario;
        }

        public Usuario? GetById(Guid usuarioId)
        {
            Usuario? usuario = _usuarios.Include(u => u.Empresa).FirstOrDefault(u => u.Id == usuarioId);

            return usuario;
        }
    }
}
