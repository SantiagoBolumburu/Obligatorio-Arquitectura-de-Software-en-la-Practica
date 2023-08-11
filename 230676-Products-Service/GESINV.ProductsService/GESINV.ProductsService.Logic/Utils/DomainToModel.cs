using GESINV.ProductsService.Dominio;
using GESINV.ProductsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.Logic.Utils
{
    public static class DomainToModel
    {
        public static VentaModelOut CombertirAModel(Venta venta)
        {
            VentaModelOut ventaModel = new VentaModelOut()
            {
                Id = venta.Id,
                MontoTotalEnPesos = venta.MontoTotalEnPesos,
                FechaVenta = venta.FechaVenta,
                NombreCliente = venta.NombreCliente,
                ProductosYCantidad = ConvertirProductoYCantidadATupla(venta.DetallesVentasProductos),
                Programada = venta.Programada,
                Realizada = venta.Realizada
            };

            return ventaModel;
        }

        public static List<Tuple<Guid, string, int>> ConvertirProductoYCantidadATupla(List<DetalleVentaProducto> ventaProductos)
        {
            List<Tuple<Guid, string, int>> tuplas = new List<Tuple<Guid, string, int>>();

            if (ventaProductos is not null)
                tuplas = ventaProductos.Select(p => new Tuple<Guid, string, int>(p.ProductoId, p.Producto.Nombre, p.Cantidad) { }).ToList();

            return tuplas;
        }

        public static ProveedorModelOut ConvertirProveedorAModelo(Proveedor proveedor)
        {
            ProveedorModelOut proveedorModel = new ProveedorModelOut()
            {
                Id = proveedor.Id,
                Nombre = proveedor.Nombre,
                Direccion = proveedor.Direccion,
                Telefono = proveedor.Telefono,
                Email = proveedor.Email,
            };

            return proveedorModel;
        }

        public static ProductoModelOut ConvertirProductoAModelo(Producto producto)
        {
            ProductoModelOut productoModel = new ProductoModelOut()
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                ImagenPath = producto.ImagenPath,
                CantidadEnInventarioInicial = producto.CantidadEnInventarioInicial,
                Precio = producto.Precio,
                CantidadComprada = producto.CantidadComprada,
                CantidadVendida = producto.CantidadVendida,
                CantidadEnInventario = producto.CantidadEnInventario()
            };

            return productoModel;
        }

        public static CompraModelOut ConvertirAModelo(Compra compra)
        {
            CompraModelOut compraModel = new CompraModelOut()
            {
                Id = compra.Id,
                NombreProveedor = compra.Proveedor.Nombre,
                CostoTotal = compra.CostoTotalEnPesos,
                FechaCompra = compra.FechaCompra,
                ProveedorId = compra.ProveedorId,
                ProductosNombreYCantidad = ConvertirProductoYCantidadATupla(compra.DetallesComprasProductos)
            };

            return compraModel;
        }

        public static List<Tuple<Guid, string, int>> ConvertirProductoYCantidadATupla(List<DetalleCompraProducto> productosCantidades)
        {
            List<Tuple<Guid, string, int>> tuplas = new List<Tuple<Guid, string, int>>();

            if (productosCantidades is not null)
            {
                tuplas = productosCantidades.Select(p =>
                    new Tuple<Guid, string, int>(p.ProductoId, p.Producto.Nombre, p.Cantidad) { }).ToList();
            }

            return tuplas;
        }
    }
}
