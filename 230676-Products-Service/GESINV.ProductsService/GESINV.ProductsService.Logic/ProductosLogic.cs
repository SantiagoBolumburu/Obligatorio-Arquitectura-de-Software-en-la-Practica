using GESINV.IdentityHandler;
using GESINV.IdentityHandler.Abstractions;
using GESINV.ProductsService.BackingServices.Interface;
using GESINV.ProductsService.BackingServices.Interface.Clases;
using GESINV.ProductsService.Dominio;
using GESINV.ProductsService.Logic;
using GESINV.ProductsService.Logic.Interface;
using GESINV.ProductsService.Logic.Utils;
using GESINV.ProductsService.Models;
using GESINV.ProductsService.PersistanceAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.Logic
{
    public class ProductosLogic : IProductosLogic
    {
        public readonly ITokenHandler _tokenHandler;
        public readonly IProductosRepository _productosRepositorio;
        public readonly IEventPublisher _eventPublisher;
        private readonly int CANTIDAD_PRODUCTOS_MAS_VENDIDOS_POR_DEFECTO = 3;

        public ProductosLogic(ITokenHandler tokenHandler, IProductosRepository productosRepositorio, 
            IEventPublisher eventPublisher)
        {
            _tokenHandler = tokenHandler;
            _productosRepositorio = productosRepositorio;
            _eventPublisher = eventPublisher;
        }

        public ProductoModelOut CrearProducto(ProductoModel productoModel)
        {
            productoModel.Validar();

            SessionInfo sessionInfo = _tokenHandler.GetCurrentSessionInfo();

            Producto producto = new Producto()
            {
                Id = Guid.NewGuid(),
                Accesible = true,
                Nombre = productoModel.Nombre,
                Descripcion = productoModel.Descripcion,
                ImagenPath = productoModel.ImagenPath,
                CantidadEnInventarioInicial = productoModel.CantidadEnInventario,
                CantidadComprada = 0,
                CantidadVendida = 0,
                Precio = productoModel.Precio,
                EmpresaId = sessionInfo.EmpresaId
            };

            Producto productoDb = _productosRepositorio.Create(producto);

            _eventPublisher.PublishNuevoProducto(new NuevoProducto() { ProductoId = producto.Id });

            ProductoModelOut model = DomainToModel.ConvertirProductoAModelo(productoDb);

            return model;
        }

        public void EliminarProducto(Guid productoId)
        {
            Guid empresaId = _tokenHandler.GetCurrentSessionInfo().EmpresaId;

            Producto producto = _productosRepositorio.GeyById_AndEmpresaId_ProductoAccesible(productoId, empresaId)
                ?? throw new ArgumentException("El producto que intenta eliminar no se encuaentra en el sistema.");

            producto.Accesible = false;

            _productosRepositorio.Update(producto);
        }

        public ProductoModelOut ModificarProducto(Guid productoId, ProductoModel productoModel)
        {
            productoModel.Validar();

            Guid empresaId = _tokenHandler.GetCurrentSessionInfo().EmpresaId;

            Producto producto = _productosRepositorio.GeyById_AndEmpresaId_ProductoAccesible(productoId, empresaId)
                ?? throw new ArgumentException("El producto que intenta modificar no se encuentra en el sistema.");

            producto.Nombre = productoModel.Nombre;
            producto.Descripcion = productoModel.Descripcion;
            producto.ImagenPath = productoModel.ImagenPath;
            producto.Precio = productoModel.Precio;
            producto.CantidadEnInventarioInicial = productoModel.CantidadEnInventario;

            Producto productoDb = _productosRepositorio.Update(producto);

            ProductoModelOut model = DomainToModel.ConvertirProductoAModelo(productoDb);

            return model;
        }

        public ProductoModelOut? ObtenerProductoDeLaEmpresaDeUsuarioLoggeado(Guid productoId)
        {
            Guid empresaId = _tokenHandler.GetCurrentSessionInfo().EmpresaId;

            Producto? producto = _productosRepositorio.GeyById_AndEmpresaId_ProductoAccesible(productoId, empresaId);

            ProductoModelOut? model = null;

            if (producto is not null)
                model = DomainToModel.ConvertirProductoAModelo(producto);

            return model;
        }

        public List<ProductoModelOut> ObtenerProductosMasVendidos(int? cantidad)
        {
            if (cantidad == null)
                cantidad = CANTIDAD_PRODUCTOS_MAS_VENDIDOS_POR_DEFECTO;

            Guid empresaId = _tokenHandler.GetCurrentSessionInfo().EmpresaId;

            List<Producto> productos = _productosRepositorio.GetByEmpresaId_ProductosMasVendidos(empresaId, cantidad.Value);

            List<ProductoModelOut> productoModels = productos.Select(p => DomainToModel.ConvertirProductoAModelo(p)).ToList();

            return productoModels;
        }

        public List<ProductoModelOut> ObtenerTodosLosProductosDeLaEmpresaDeUsuarioLoggeado()
        {
            Guid empresaId = _tokenHandler.GetCurrentSessionInfo().EmpresaId;

            List<Producto> productos = _productosRepositorio.GetByEmpresaId_ProductosAccesibles(empresaId);

            List<ProductoModelOut> models = productos.Select(p => DomainToModel.ConvertirProductoAModelo(p)).ToList();

            return models;
        }

        public void EmitirTodosLosPorductos()
        {
            Guid empresaId = _tokenHandler.GetCurrentSessionInfo().EmpresaId;

            List<Producto> productos = _productosRepositorio.GetByEmpresaId_ProductosAccesibles(empresaId);

            productos.ForEach(p =>
            {
                _eventPublisher.PublishNuevoProducto(new NuevoProducto() { ProductoId = p.Id });
            });
        }
    }
}

