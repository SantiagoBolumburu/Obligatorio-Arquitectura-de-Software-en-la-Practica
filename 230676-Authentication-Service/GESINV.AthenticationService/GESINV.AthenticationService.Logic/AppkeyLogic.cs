using GESINV.AthenticationService.BackingServicesAccess.Abstractions;
using GESINV.AthenticationService.Dominio;
using GESINV.AthenticationService.Logic.Interface;
using GESINV.AthenticationService.PersistanceAccess.Interface;
using GESINV.AthenticationService.Utils;
using GESINV.IdentityHandler;
using GESINV.IdentityHandler.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GESINV.AthenticationService.Logic
{
    public class AppkeyLogic : IAppkeyLogic
    {
        private readonly ITokenHandler _tokenHandler;
        private readonly IAppkeyRepository _appkeyRepository;
        private readonly IKeyValueStorage _keyValueStorage;
        private readonly ManejadorDeConfiguracion _manejadorDeConfiguracion;

        private readonly int SEGUNDOS_DE_INACTIVIDAD_HASTA_TIMEOUT_APPKEY;

        public AppkeyLogic(ITokenHandler tokenHandler, IAppkeyRepository appkeyRepository,
            IKeyValueStorage keyValueStorage, ManejadorDeConfiguracion manejadorDeConfiguracion) 
        {
            _tokenHandler = tokenHandler;
            _appkeyRepository = appkeyRepository;
            _keyValueStorage = keyValueStorage;
            _manejadorDeConfiguracion = manejadorDeConfiguracion;

            SEGUNDOS_DE_INACTIVIDAD_HASTA_TIMEOUT_APPKEY = _manejadorDeConfiguracion.ObtenerTiempoSegundosHastaTimeoutDeApikey();
        }

        public string CrearAppKey()
        {
            SessionInfo sessionInfo = _tokenHandler.GetCurrentSessionInfo();

            ApplicationKey? appKeyActiva = _appkeyRepository.GetActiveByUserId(sessionInfo.UsuarioId);
            if (appKeyActiva != null)
                EliminarAppKeyAnterior(sessionInfo.UsuarioId);


            Guid sessionId = Guid.NewGuid();
            SessionInfo appKeySessionInfo = new SessionInfo()
            {
                SessionId = sessionId,
                UsuarioId = sessionInfo.UsuarioId,
                EmpresaId = sessionInfo.EmpresaId,
                Nombre = "APP_KEY_" + sessionInfo.Nombre,
                Empresa = sessionInfo.Empresa,
                Email = "not_applicable",
                TiempoCreacion = DateTime.Now,
                Rol = Roles.RolAplicacion
            };

            string secretKey = Environment.GetEnvironmentVariable(EnvarionmentVariablesNames.GESINV_JWT_SECRET)
                ?? throw new InvalidOperationException();
            string appkeyStr = _tokenHandler.GetSessionToken(appKeySessionInfo, secretKey);

            ApplicationKey appkeyObj = new ApplicationKey()
            {
                Id = Guid.NewGuid(),
                FechaCreacion = appKeySessionInfo.TiempoCreacion,
                UsuarioId = appKeySessionInfo.UsuarioId,
                EmpresaId = appKeySessionInfo.EmpresaId,
                SessionId = appKeySessionInfo.SessionId,
                ApplicationKeyStr = appkeyStr,
                Activa = true
            };


            _appkeyRepository.Create(appkeyObj);

            _keyValueStorage.Crear(sessionId.ToString(), appkeyStr, SEGUNDOS_DE_INACTIVIDAD_HASTA_TIMEOUT_APPKEY);
            
            return appkeyStr;
        }

        public string ObtenerAppkeyActual()
        {
            SessionInfo sessionInfo = _tokenHandler.GetCurrentSessionInfo();

            ApplicationKey? appKey = _appkeyRepository.GetActiveByUserId(sessionInfo.UsuarioId);
            string appKeyString = "";

            if (appKey != null)
                appKeyString = appKey.ApplicationKeyStr;
            
            return appKeyString;
        }

        public void TeminarApiKey()
        {
            SessionInfo sessionInfo = _tokenHandler.GetCurrentSessionInfo();

            List<ApplicationKey> appKeys = _appkeyRepository.GetAllActivesByUserId(sessionInfo.UsuarioId);

            appKeys.ForEach(appKey =>
            {
                appKey.Activa = false;
                _appkeyRepository.Update(appKey);

                _keyValueStorage.Borrar(appKey.SessionId.ToString()).Wait();
            });
        }

        private void EliminarAppKeyAnterior(Guid UsuarioId)
        {
            TeminarApiKey();

            _keyValueStorage.Borrar(UsuarioId.ToString()).Wait();
        }
    }
}