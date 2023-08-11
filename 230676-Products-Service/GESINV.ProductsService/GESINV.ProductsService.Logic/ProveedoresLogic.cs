using GESINV.IdentityHandler;
using GESINV.IdentityHandler.Abstractions;
using GESINV.ProductsService.Dominio;
using GESINV.ProductsService.Logic.Interface;
using GESINV.ProductsService.Logic.Utils;
using GESINV.ProductsService.Models;
using GESINV.ProductsService.PersistanceAccess.Interface;
using GESINV.ProductsService.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.Logic
{
    public class ProveedoresLogic : IProveedoresLogic
    {
        public readonly ITokenHandler _tokenHandler;
        public readonly IProveedoresRepository _proveedoresRepositorio;

        public ProveedoresLogic(ITokenHandler tokenHandler, IProveedoresRepository proveedoresRepositorio)
        {
            _tokenHandler = tokenHandler;
            _proveedoresRepositorio = proveedoresRepositorio;
        }

        public ProveedorModelOut CrearProveedor(ProveedorModel proveedorModel)
        {
            proveedorModel.Validar();
            if (!StringUtils.FormatoEmailEsValido(proveedorModel.Email))
                throw new ArgumentException("El email no tiene formato valido.");
            if (!int.TryParse(proveedorModel.Telefono, out int n))
                throw new ArgumentException("El telefono no puede tener caracteres no numericos.");

            SessionInfo sessionInfo = _tokenHandler.GetCurrentSessionInfo();

            Proveedor proveedor = new Proveedor()
            {
                Id = Guid.NewGuid(),
                Accesible = true,
                Nombre = proveedorModel.Nombre,
                Direccion = proveedorModel.Direccion,
                Email = proveedorModel.Email,
                Telefono = proveedorModel.Telefono,
                EmpresaId = sessionInfo.EmpresaId
            };

            Proveedor proveedorDb = _proveedoresRepositorio.Crear(proveedor);

            ProveedorModelOut model = DomainToModel.ConvertirProveedorAModelo(proveedorDb);

            return model;
        }

        public void EliminarProveedor(Guid proveedorId)
        {
            SessionInfo sessionInfo = _tokenHandler.GetCurrentSessionInfo();

            Proveedor proveedor = _proveedoresRepositorio.ObtenerPorIdYEmpresaIdProveedorAccesible(proveedorId, sessionInfo.EmpresaId)
                ?? throw new ArgumentException("El proveedor seleccionado no se encuentra en el sistema.");

            proveedor.Accesible = false;

            _proveedoresRepositorio.Modificar(proveedor);
        }

        public ProveedorModelOut ModificarProveedor(Guid proveedorId, ProveedorModel proveedorModel)
        {
            proveedorModel.Validar();

            SessionInfo sessionInfo = _tokenHandler.GetCurrentSessionInfo();

            Proveedor proveedor = _proveedoresRepositorio.ObtenerPorIdYEmpresaIdProveedorAccesible(proveedorId, sessionInfo.EmpresaId)
                ?? throw new ArgumentException("El proveedor seleccionado no se encuentra en el sistema.");

            proveedor.Nombre = proveedorModel.Nombre;
            proveedor.Direccion = proveedorModel.Direccion;
            proveedor.Email = proveedorModel.Email;
            proveedor.Telefono = proveedorModel.Telefono;

            Proveedor proveedorDb = _proveedoresRepositorio.Modificar(proveedor);

            ProveedorModelOut model = DomainToModel.ConvertirProveedorAModelo(proveedorDb);

            return model;
        }

        public ProveedorModelOut? ObtenerProveedorDeLaEmpresaDeUsuarioLoggeado(Guid proveedorId)
        {
            SessionInfo sessionInfo = _tokenHandler.GetCurrentSessionInfo();

            Proveedor? proveedor = _proveedoresRepositorio.ObtenerPorIdYEmpresaIdProveedorAccesible(proveedorId, sessionInfo.EmpresaId);

            ProveedorModelOut? model = null;

            if (proveedor is not null)
                model = DomainToModel.ConvertirProveedorAModelo(proveedor);

            return model;
        }

        public List<ProveedorModelOut> ObtenerTodosLosProveedoresDeLaEmpresaDeUsuarioLoggeado()
        {
            SessionInfo sessionInfo = _tokenHandler.GetCurrentSessionInfo();

            List<Proveedor> proveedores = _proveedoresRepositorio.ObtenerPorEmpresaIdProveedoresAccesibles(sessionInfo.EmpresaId);

            List<ProveedorModelOut> models = proveedores.Select(p => DomainToModel.ConvertirProveedorAModelo(p)).ToList();

            return models;
        }
    }
}
