using GESINV.AthenticationService.PersistanceAccess.Interface;
using GESINV.AthenticationService.Dominio;
using GESINV.AthenticationService.Logic.Interface;
using GESINV.AthenticationService.Models;
using GESINV.AthenticationService.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GESINV.AthenticationService.BackingServicesAccess.Abstractions;
using GESINV.IdentityHandler.Abstractions;

namespace GESINV.AthenticationService.Logic
{
    public class InvitacionesLogic : IInvitacionesLogic
    {
        private readonly IEmailSender _emailSender;
        private readonly IUsuariosRepository _usuariosRepositorio;
        private readonly IInvitacionesRepository _invitacionesRepositorio;
        private readonly ITokenHandler _tokenHandler;
        private readonly ManejadorDeConfiguracion _manejadorDeConfiguracion;

        public InvitacionesLogic(IUsuariosRepository usuariosRepositorio, IInvitacionesRepository invitacionesRepositorio,
            ITokenHandler tokenHandler, IEmailSender emailSender, ManejadorDeConfiguracion manejadorDeConfiguracion)
        {
            _usuariosRepositorio = usuariosRepositorio;
            _invitacionesRepositorio = invitacionesRepositorio;
            _manejadorDeConfiguracion = manejadorDeConfiguracion;
            _tokenHandler = tokenHandler;
            _emailSender = emailSender;
        }

        public InvitacionOutModel Crear(InvitacionModel invitacionModel)
        {
            if (!Roles.EsValidoParaUsuario(invitacionModel.Rol))
                throw new ArgumentException("El Rol seleccionado o no existe o no es valido para usuarios.");
            if (!StringUtils.IsValidEmailFormat(invitacionModel.Email))
                throw new ArgumentException("El email no tiene formato valido.");

            Guid id = _tokenHandler.GetCurrentSessionInfo().UsuarioId;
            Usuario? invitador = _usuariosRepositorio.GetById(id) ?? throw new ArgumentException();

            Empresa empresa = invitador.Empresa;


            Invitacion invitacion = new Invitacion()
            {
                Id = Guid.NewGuid(),
                Email = invitacionModel.Email,
                Rol = invitacionModel.Rol,
                Utilizada = false,
                Empresa = empresa,
                EmpresaId = empresa.Id,
                Invitador = invitador,
                InvitadorId = invitador.Id,
                FechaVencimiento = DateTime.Today.AddDays(_manejadorDeConfiguracion.ObtenerDiasHastaVencimientoDeInvitacion())
            };

            Invitacion invitacionBD = _invitacionesRepositorio.Create(invitacion);

            EnviarCorreoInvitacion(invitacionBD);

            InvitacionOutModel invitacionOutModel = new InvitacionOutModel()
            {
                InvitacionId = invitacionBD.Id.ToString(),
                NombreEmpresa = invitacionBD.Empresa.Nombre,
                ValidaHasta = invitacion.FechaVencimiento.ToString()
            };

            return invitacionOutModel;
        }

        public InvitacionOutModel? Obtener(Guid invitacionId)
        {
            Invitacion? invitacion = _invitacionesRepositorio.GetById(invitacionId);
            InvitacionOutModel? invitacionOutModel = null;

            if (invitacion != null)
            {
                invitacionOutModel = new InvitacionOutModel()
                {
                    InvitacionId = invitacion.Id.ToString(),
                    NombreEmpresa = invitacion.Empresa.Nombre,
                    ValidaHasta = invitacion.FechaVencimiento.ToString()
                };
            }

            return invitacionOutModel;
        }

        private void EnviarCorreoInvitacion(Invitacion invitacion)
        {
            string linkFrontEnd = Environment.GetEnvironmentVariable(EnvarionmentVariablesNames.GESINV_FRONTEND_LINK_INVITACIONES)
                ?? throw new Exception();

            string asunto = "Invitacion a empresa en GESINV";
            string cuerpo = $@"El usuario -{invitacion.Invitador.Nombre}- te han invitado a GESINV, para que empieses a colaborar en la empresa: --{invitacion.Empresa.Nombre}--.
                                Registrate a traves del siguiente enlace: {linkFrontEnd}{invitacion.Id}";


            _emailSender.EnviarCorreo(invitacion.Invitador.Email, invitacion.Email, asunto, cuerpo);
        }
    }
}
