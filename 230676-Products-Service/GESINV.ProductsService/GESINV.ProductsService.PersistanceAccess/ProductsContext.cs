using GESINV.ProductsService.Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.ProductsService.PersistanceAccess
{
    public class ProductsContext : DbContext
    {
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<DetalleCompraProducto> DetallesComprasProductos { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVentaProducto> DetallesVentasProductos { get; set; }

        public ProductsContext() { }
        public ProductsContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>().HasKey(i => i.Id);
            modelBuilder.Entity<Producto>().Property(i => i.Nombre).IsRequired();
            modelBuilder.Entity<Producto>().Property(i => i.Descripcion).IsRequired();
            modelBuilder.Entity<Producto>().Property(i => i.Precio).IsRequired();
            modelBuilder.Entity<Producto>().Property(i => i.CantidadEnInventarioInicial).IsRequired();
            modelBuilder.Entity<Producto>().Property(i => i.ImagenPath).IsRequired();
            modelBuilder.Entity<Producto>().Property(i => i.Accesible).IsRequired();
            modelBuilder.Entity<Producto>().Property(i => i.EmpresaId).IsRequired();
            modelBuilder.Entity<Producto>().HasIndex(p => new { p.EmpresaId, p.Nombre }).IsUnique();
            modelBuilder.Entity<Producto>().HasMany<DetalleCompraProducto>(p => p.DetallesComprasProductos).WithOne(pc => pc.Producto).HasForeignKey(pc => pc.ProductoId);
            modelBuilder.Entity<Producto>().HasMany<DetalleVentaProducto>(p => p.DetallesVentasProductos).WithOne(pc => pc.Producto).HasForeignKey(pc => pc.ProductoId);
            modelBuilder.Entity<Producto>().Property(p => p.CantidadVendida).IsRequired();
            modelBuilder.Entity<Producto>().Property(p => p.CantidadComprada).IsRequired();


            modelBuilder.Entity<DetalleCompraProducto>().HasKey(p => p.Id);
            modelBuilder.Entity<DetalleCompraProducto>().Property(p => p.Cantidad).IsRequired();
            
            modelBuilder.Entity<Compra>().HasKey(c => c.Id);
            modelBuilder.Entity<Compra>().Property(c => c.FechaCompra).IsRequired();
            modelBuilder.Entity<Compra>().Property(c => c.CostoTotalEnPesos).IsRequired();
            modelBuilder.Entity<Compra>().HasMany<DetalleCompraProducto>(c => c.DetallesComprasProductos).WithOne(pc => pc.Compra).HasForeignKey(pc => pc.CompraId);


            modelBuilder.Entity<DetalleVentaProducto>().HasKey(p => p.Id);
            modelBuilder.Entity<DetalleVentaProducto>().Property(p => p.Cantidad).IsRequired();

            modelBuilder.Entity<Venta>().HasKey(v => v.Id);
            modelBuilder.Entity<Venta>().Property(v => v.FechaVenta).IsRequired();
            modelBuilder.Entity<Venta>().Property(v => v.NombreCliente).IsRequired();
            modelBuilder.Entity<Venta>().Property(v => v.MontoTotalEnPesos).IsRequired();
            modelBuilder.Entity<Venta>().Property(v => v.Programada).IsRequired();
            modelBuilder.Entity<Venta>().Property(v => v.Realizada).IsRequired();
            modelBuilder.Entity<Venta>().HasMany<DetalleVentaProducto>(v => v.DetallesVentasProductos).WithOne(vp => vp.Venta).HasForeignKey(vp => vp.VentaId);


            modelBuilder.Entity<Proveedor>().HasKey(i => i.Id);
            modelBuilder.Entity<Proveedor>().Property(i => i.Nombre).IsRequired();
            modelBuilder.Entity<Proveedor>().Property(i => i.Direccion).IsRequired();
            modelBuilder.Entity<Proveedor>().Property(i => i.Email).IsRequired();
            modelBuilder.Entity<Proveedor>().Property(i => i.Telefono).IsRequired();
            modelBuilder.Entity<Proveedor>().Property(i => i.Accesible).IsRequired();
            modelBuilder.Entity<Proveedor>().Property(i => i.EmpresaId).IsRequired();
            modelBuilder.Entity<Proveedor>().HasIndex(p => new { p.EmpresaId, p.Nombre }).IsUnique();
            modelBuilder.Entity<Proveedor>().HasMany<Compra>(p => p.Compras).WithOne(c => c.Proveedor).HasForeignKey(c => c.ProveedorId);
        }
    }
}
