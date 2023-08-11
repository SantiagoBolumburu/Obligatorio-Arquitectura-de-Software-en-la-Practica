using GESINV.AthenticationService.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.AthenticationService.PersistanceAccess.Interface
{
    public interface IUsuariosRepository
    {
        bool ExistsByEmail(string email);
        Usuario Add(Usuario usuario);
        Usuario? GetByCredentials(string email, string password);
        Usuario? GetById(Guid id);
    }
}
