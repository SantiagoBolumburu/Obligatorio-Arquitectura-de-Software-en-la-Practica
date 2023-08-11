using GESINV.SubscriptionsService.Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.SubscriptionsService.DataAccess
{
    public class SubscriptionsContext : DbContext
    {
        public DbSet<Producto> Productos { get; set; }
        public DbSet<CompraVentaSubscription> CompraVentaSubscriptions { get; set; }
        public DbSet<StockSubscription> StockSubscriptions { get; set; }

        public SubscriptionsContext() { }
        public SubscriptionsContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompraVentaSubscription>().HasKey(c => c.Id);
            modelBuilder.Entity<CompraVentaSubscription>().HasIndex(c => new { c.ProductoId, c.UsuarioId}).IsUnique();

            modelBuilder.Entity<StockSubscription>().HasKey(s => s.Id);
            modelBuilder.Entity<StockSubscription>().HasIndex(s => new { s.ProductoId, s.UsuarioId }).IsUnique();

            modelBuilder.Entity<Producto>().HasKey(e => e.Id);
            modelBuilder.Entity<Producto>().HasIndex(p => p.ProductoMainId).IsUnique();
            modelBuilder.Entity<Producto>().HasMany<StockSubscription>(p => p.StockSubscriptions)
                .WithOne(s => s.Producto).HasForeignKey(s => s.ProductoId);
            modelBuilder.Entity<Producto>().HasMany<CompraVentaSubscription>(p => p.CompraVentasSubscriptions)
                .WithOne(s => s.Producto).HasForeignKey(s => s.ProductoId);
        }
    }
}
