using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.AthenticationService.Models
{
    public class CredencialesModel
    {
        public string Email { get; set; }
        public string Contrasenia { get; set; }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Contrasenia))
                throw new ArgumentNullException("Las credensiales no son validas, debe ingresa un valor en ambos campos.");
        }
    }
}
