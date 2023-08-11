using GESINV.AthenticationService.PersistanceAccess.Interface;
using GESINV.AthenticationService.Dominio;
using GESINV.AthenticationService.Logic.Interface;
using GESINV.AthenticationService.Models;
using GESINV.AthenticationService.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.AthenticationService.Logic
{
    public class UsuariosLogic : IUsuariosLogic
    {
        public readonly IUsuariosRepository _usuariosRepositorio;
        public readonly IEmpresasRepository _empresasRepositorio;
        public readonly IInvitacionesRepository _invitacionesRepositorio;

        public UsuariosLogic(IUsuariosRepository usuariosRepositorio, IEmpresasRepository empresasRepositorio,
            IInvitacionesRepository invitacionesRepositorio) 
        {
            _usuariosRepositorio = usuariosRepositorio;
            _empresasRepositorio = empresasRepositorio;
            _invitacionesRepositorio = invitacionesRepositorio;
        }

        public UsuarioModel CreateAdminAndEmpresa(UsuarioModel usuario)
        {
            usuario.ValidateAdmin();

            if (!StringUtils.IsValidEmailFormat(usuario.Email))
                throw new ArgumentException("El email no tiene formato valido.");

            if (_usuariosRepositorio.ExistsByEmail(usuario.Email))
                throw new ArgumentException("El email ingresado ya esta en uso.");
            if (_empresasRepositorio.ExistByName(usuario.NombreEmpresa))
                throw new ArgumentException("El nombre de empresa ingresado ya esta en uso.");


            Empresa nuevaEmpresa = _empresasRepositorio.Add(
                new Empresa
                {
                    Id = Guid.NewGuid(),
                    Nombre = usuario.NombreEmpresa
                }
            );

            Usuario nuevoAdmin = _usuariosRepositorio.Add(
                new Usuario
                {
                    Id = Guid.NewGuid(),
                    Nombre = usuario.Nombre,
                    Email = usuario.Email,
                    Contrasenia = usuario.Contrasenia,
                    Empresa = nuevaEmpresa,
                    Rol = Roles.RolAdministrador
                }
            );

            UsuarioModel nuevoUsuarioModel = new UsuarioModel
            {
                Nombre = nuevoAdmin.Nombre,
                Contrasenia = "***",
                Email = nuevoAdmin.Email,
                NombreEmpresa = nuevoAdmin.Empresa.Nombre
            };

            return nuevoUsuarioModel;
        }


        public UsuarioModel CreateByInvitacion(UsuarioModel usuarioModel, Guid invitacionid)
        {
            usuarioModel.ValidateByInvitacion();

            Invitacion invitacion = _invitacionesRepositorio.GetById(invitacionid)
                ?? throw new ArgumentException("La invitacion no existe en el sistema.");       
            if (invitacion.FechaVencimiento < DateTime.Today || invitacion.Utilizada)
                throw new ArgumentException("La invitacion caduco o ya fue utilizada.");


            if (!StringUtils.IsValidEmailFormat(usuarioModel.Email))
                throw new ArgumentException("El email no tiene formato valido.");

            if (_usuariosRepositorio.ExistsByEmail(usuarioModel.Email))
                throw new ArgumentException("El email ingresado ya esta en uso.");


            Usuario nuevoUsuario = _usuariosRepositorio.Add(
                new Usuario
                {
                    Id = Guid.NewGuid(),
                    Nombre = usuarioModel.Nombre,
                    Email = usuarioModel.Email,
                    Contrasenia = usuarioModel.Contrasenia,
                    Empresa = invitacion.Empresa,
                    Rol = invitacion.Rol
                }
            );

            
            invitacion.Utilizada = true;
            _invitacionesRepositorio.Update(invitacion);


            UsuarioModel nuevoUsuarioModel = new UsuarioModel
            {
                Nombre = nuevoUsuario.Nombre,
                Contrasenia = "****",
                Email = nuevoUsuario.Email,
                NombreEmpresa = nuevoUsuario.Empresa.Nombre
            };

            return nuevoUsuarioModel;
        }
    }
}
