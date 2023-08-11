using GESINV.AthenticationService.BackingServicesAccess.Abstractions;
using GESINV.AthenticationService.Dominio;
using GESINV.AthenticationService.Logic.Interface;
using GESINV.AthenticationService.Models;
using GESINV.AthenticationService.PersistanceAccess.Interface;
using GESINV.AthenticationService.Utils;
using GESINV.IdentityHandler;
using GESINV.IdentityHandler.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GESINV.AthenticationService.Logic
{
    public class SessionsLogic : ISessionsLogic
    {
        private readonly ITokenHandler _tokenHandler;
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly ManejadorDeConfiguracion _manejadorConfiguracion;
        private readonly IKeyValueStorage _cacheAccess;

        private readonly int SEGUNDOS_DE_INACTIVIDAD_HASTA_TIMEOUT;

        public SessionsLogic(ITokenHandler tokenHandler, IUsuariosRepository usuariosRepository,
            ManejadorDeConfiguracion manejadorConfiguracion, IKeyValueStorage cacheAcces)
        {
            _tokenHandler = tokenHandler;
            _usuariosRepository = usuariosRepository;
            _manejadorConfiguracion = manejadorConfiguracion;
            _cacheAccess = cacheAcces;

            SEGUNDOS_DE_INACTIVIDAD_HASTA_TIMEOUT = _manejadorConfiguracion.ObtenerTiempoSegundosHastaTimeoutDeSesion();
        }

        public string CrearSession(CredencialesModel credenciales)
        {
            credenciales.Validar();

            Usuario usuario = _usuariosRepository.GetByCredentials(credenciales.Email, credenciales.Contrasenia)
                ?? throw new ArgumentException("No hay usuario en el sistema con las credenciales ingresadas.");

            Guid sessionId = Guid.NewGuid();

            SessionInfo sessionInfo = new SessionInfo()
            {
                SessionId = sessionId,
                EmpresaId = usuario.Empresa.Id,
                UsuarioId = usuario.Id,
                TiempoCreacion = DateTime.Now,
                Empresa = usuario.Empresa.Nombre,
                Email = usuario.Email,
                Nombre = usuario.Nombre,
                Rol = usuario.Rol
            };

            string secretKey = Environment.GetEnvironmentVariable(EnvarionmentVariablesNames.GESINV_JWT_SECRET)
                ?? throw new InvalidOperationException();

            string token = _tokenHandler.GetSessionToken(sessionInfo, secretKey);

            GuardarSessionEnMemoria(sessionId.ToString(), token);

            return token;
        }

        public void TeminarSession()
        {
            SessionInfo sessionInfo = _tokenHandler.GetCurrentSessionInfo();

            _cacheAccess.Borrar(sessionInfo.SessionId.ToString());
        }

        public Guid ObtenerIdUsaurioLoggeado()
        {
            SessionInfo sessionInfo = _tokenHandler.GetCurrentSessionInfo();

            return sessionInfo.SessionId;
        }

        public void RenovarSession(Guid sessionId)
        {
            _cacheAccess.ReiniciarExpiracion(sessionId.ToString(), SEGUNDOS_DE_INACTIVIDAD_HASTA_TIMEOUT).Wait();
        }

        public bool ExisteSession(Guid sessionId)
        {
            string? session = _cacheAccess.Obtener(sessionId.ToString()).Result;

            if (session is null)
                return false;
            return true;
        }


        private void GuardarSessionEnMemoria(string sessionId, string info)
        {
            _cacheAccess.Crear(sessionId, info, SEGUNDOS_DE_INACTIVIDAD_HASTA_TIMEOUT).Wait();
        }
    }
}
