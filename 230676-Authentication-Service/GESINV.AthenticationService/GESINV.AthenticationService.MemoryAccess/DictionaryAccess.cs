using GESINV.AthenticationService.MemoryAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.AthenticationService.MemoryAccess
{
    internal class DictionaryAccess : IKeyValueStorage
    {
        private Dictionary<string, (string, DateTime)> _pseudoCache;
        public DictionaryAccess()
        {
            _pseudoCache = new Dictionary<string, (string, DateTime)>();
        }

        public Task Borrar(string llave)
        {
            _pseudoCache.Remove(llave);

            return Task.CompletedTask;
        }

        public Task Crear(string llave, string valor, int segundosDeVida)
        {
            Task crear = new Task(() => _pseudoCache.Add(llave, (valor, DateTime.Now.AddSeconds(segundosDeVida))));

            crear.Start();

            return crear;
        }

        public Task<string?> Obtener(string llave)
        {
            Task<string?> obtener = new Task<string?>(() =>
            {
                string? toReturn;
                (string, DateTime) valor = _pseudoCache.GetValueOrDefault(llave);
                if (valor.Item1 != null && valor.Item2 > DateTime.Now)
                    toReturn = valor.Item1;
                else
                    toReturn = null;

                return toReturn;
            });

            obtener.Start();

            return obtener;
        }

        public Task ReiniciarExpiracion(string llave, int segundosDeVida)
        {
            Task reinicio = new Task(() => {
                (string, DateTime) valor;
                if (_pseudoCache.TryGetValue(llave, out valor))
                {
                    _pseudoCache[llave] = (valor.Item1, valor.Item2.AddSeconds(segundosDeVida));
                }
            });

            reinicio.Start();

            return reinicio;
        }
    }
}
