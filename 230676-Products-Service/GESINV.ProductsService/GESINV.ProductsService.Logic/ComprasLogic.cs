using GESINV.IdentityHandler;
using GESINV.IdentityHandler.Abstractions;
using GESINV.ProductsService.BackingServices.Interface.Clases;
using GESINV.ProductsService.BackingServices.Interface;
using GESINV.ProductsService.Dominio;
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
    public class ComprasLogic : IComprasLogic
    {
        private readonly ITokenHandler _tokenHandler;
        private readonly IEventPublisher _eventPublisher;
        private readonly IProveedoresRepository _proveedoresRepositorio;
        private readonly IProductosRepository _productosRepositorio;
        private readonly IComprasRepository _comprasRepositorio;
        public ComprasLogic(ITokenHandler tokenHnadler, IProveedoresRepository proveedoresRepositorio,
            IProductosRepository productosRepositorio, IComprasRepository comprasRepositorio, IEventPublisher eventPublisher)
        {
            _tokenHandler = tokenHnadler;
            _eventPublisher = eventPublisher;
            _proveedoresRepositorio = proveedoresRepositorio;
            _productosRepositorio = productosRepositorio;
            _comprasRepositorio = comprasRepositorio;
        }

        public CompraModelOut CrearCompraEnEmpresaDeUsuarioLoggeado(CompraModel compraModel)
        {
            compraModel.Validar();

            if (HayProductosRepetidos(compraModel.ProductosYCantidad))
                throw new ArgumentException("Cada producto solo puede incluirse una vez por compra.");

            SessionInfo sessionInfo = _tokenHandler.GetCurrentSessionInfo();

            Proveedor proveedor = _proveedoresRepositorio.ObtenerPorIdYEmpresaIdProveedorAccesible(compraModel.ProveedorId, sessionInfo.EmpresaId)
                ?? throw new ArgumentException("El proveedor seleccionado no se encuentra en encuantra en el sistema o ya no es accesible.");

            List<DetalleCompraProducto> productosCantidades = ObtenerProductos(compraModel.ProductosYCantidad, sessionInfo.EmpresaId);

            Compra compra = new Compra()
            {
                Id = Guid.NewGuid(),
                FechaCompra = compraModel.FechaCompra,
                CostoTotalEnPesos = compraModel.CostoTotal,
                DetallesComprasProductos = productosCantidades,
                EmpresaId = sessionInfo.EmpresaId,
                Proveedor = proveedor,
                ProveedorId = proveedor.Id
            };

            Compra compraDb = _comprasRepositorio.Crear(compra);

            Dictionary<Guid, int> productosActualizar = compra.DetallesComprasProductos.ToDictionary(d => d.ProductoId, d => d.Cantidad);
            _productosRepositorio.UpdateCantidadCompras(productosActualizar);

            EmitirEvento(compraDb);

            CompraModelOut compraModelDb = DomainToModel.ConvertirAModelo(compraDb);

            return compraModelDb;
        }

        public CompraModelOut? ObtenerCompraDeLaEmpresaDeUsuarioLoggeado(Guid compraId)
        {
            Guid empresaId = _tokenHandler.GetCurrentSessionInfo().EmpresaId;

            Compra? compra = _comprasRepositorio.ObtenerPorIdYEmpresaId(compraId, empresaId);

            CompraModelOut? model = null;

            if (compra is not null)
                model = DomainToModel.ConvertirAModelo(compra);

            return model;
        }

        public List<CompraModelOut> ObtenerComprasAProveedorDeLaEmpresa(Guid proveedorId, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            Guid empresaId = _tokenHandler.GetCurrentSessionInfo().EmpresaId;

            fechaDesde ??= DateTime.MinValue;
            fechaHasta ??= DateTime.MaxValue;

            List<Compra> compras = _comprasRepositorio.ObtenerPorEmpresaIdNombreProveedorYFecha(empresaId, proveedorId, fechaDesde, fechaHasta);

            List<CompraModelOut> models = new List<CompraModelOut>();

            if (compras != null)
                models = compras.Select(p => DomainToModel.ConvertirAModelo(p)).ToList();

            return models;
        }

        public CompraSetUpModel ObtenerCompraSetUpsDeEmpresa()
        {
            Guid empresaId = _tokenHandler.GetCurrentSessionInfo().EmpresaId;

            List<Proveedor> proveedores = _proveedoresRepositorio.ObtenerPorEmpresaIdProveedoresAccesibles(empresaId);
            List<Producto> productos = _productosRepositorio.GetByEmpresaId_ProductosAccesibles(empresaId);

            CompraSetUpModel compraSetUpModel = new CompraSetUpModel()
            {
                Productos = productos.Select(p => DomainToModel.ConvertirProductoAModelo(p)).ToList(),
                Proveedores = proveedores.Select(p => DomainToModel.ConvertirProveedorAModelo(p)).ToList(),
            };

            return compraSetUpModel;
        }

        public List<CompraModelOut> ObtenerTodasLasComprasDeLaEmpresaDelUsaurioLoggeado()
        {
            Guid empresaId = _tokenHandler.GetCurrentSessionInfo().EmpresaId;

            List<Compra> compras = _comprasRepositorio.ObtenerPorEmpresaId(empresaId);

            List<CompraModelOut> models = new List<CompraModelOut>();

            if (compras != null)
                models = compras.Select(p => DomainToModel.ConvertirAModelo(p)).ToList();

            return models;
        }

        private bool HayProductosRepetidos(List<Tuple<Guid, int>> idsCantidades)
        {
            HashSet<Guid> diffChecker = new HashSet<Guid>();
            bool todosDiferentes = idsCantidades.All(i => diffChecker.Add(i.Item1));

            return !todosDiferentes;
        }

        private List<DetalleCompraProducto> ObtenerProductos(List<Tuple<Guid, int>> tuplasIdCantidad, Guid empresaId)
        {
            List<Producto> productos = _productosRepositorio.GetByEmpresaId_ProductosAccesibles(empresaId);

            List<DetalleCompraProducto> productosCantidades = new List<DetalleCompraProducto>();

            foreach (Tuple<Guid, int> tupla in tuplasIdCantidad)
            {
                Producto producto = productos.FirstOrDefault(p => p.Id == tupla.Item1 && p.EmpresaId == empresaId)
                    ?? throw new ArgumentException("Uno de los preductos seleccionados no se encuentra en el sistema o ya no es accesible.");

                productosCantidades.Add(new DetalleCompraProducto()
                {
                    Id = Guid.NewGuid(),
                    Cantidad = tupla.Item2,
                    Producto = producto,
                    ProductoId = producto.Id,
                    StockDespuesDeCompra = producto.CantidadEnInventario() + tupla.Item2
                });
            }

            return productosCantidades;
        }

        private void EmitirEvento(Compra compra)
        {
            List<ProductoEventInfo> productoEventInfos;

            productoEventInfos = compra.DetallesComprasProductos.Select(d => new ProductoEventInfo()
            {
                EntidadId = compra.Id,
                TipoEvento = ProductoEventInfo.TipoCompra,
                StockActual = d.StockDespuesDeCompra,
                ProductoNombre = d.Producto.Nombre,
                ProductoId = d.ProductoId,
                Descripcion = $"Hubo una compra del producto {d.Producto.Nombre}."
            }).ToList();

            productoEventInfos.ForEach(d => _eventPublisher.PublishEvent(d));
        }
    }
}
