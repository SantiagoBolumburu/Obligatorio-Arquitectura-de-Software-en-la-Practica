using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.AthenticationService.BackingServicesAccess.Abstractions
{
    public interface IKeyValueStorage
    {
        Task Crear(string llave, string valor, int segundosDeVida);

        Task<string?> Obtener(string llave);

        Task Borrar(string llave);

        Task ReiniciarExpiracion(string llave, int segundosDeVida);
        bool GetHealth();
    }
}
