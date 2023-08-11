using GESINV.AthenticationService.Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESINV.AthenticationService.PersistanceAccess
{
    public class AuthenticationContext : DbContext
    {
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Invitacion> Invitaciones { get; set; }
        public DbSet<ApplicationKey> ApplicationKeys { get; set; }

        public AuthenticationContext() { }
        public AuthenticationContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empresa>().HasKey(e => e.Id);
            modelBuilder.Entity<Empresa>().Property(e => e.Nombre).IsRequired();
            modelBuilder.Entity<Empresa>().HasIndex(e => e.Nombre).IsUnique();
            modelBuilder.Entity<Empresa>().HasMany<Usuario>(e => e.Integrantes).WithOne(i => i.Empresa);
            modelBuilder.Entity<Empresa>().HasMany<Invitacion>(e => e.Invitaciones).WithOne(i => i.Empresa).HasForeignKey(i => i.EmpresaId);


            modelBuilder.Entity<Usuario>().HasKey(u => u.Id);
            modelBuilder.Entity<Usuario>().Property(u => u.Email).IsRequired();
            modelBuilder.Entity<Usuario>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Usuario>().Property(u => u.Nombre).IsRequired();
            modelBuilder.Entity<Usuario>().Property(u => u.Contrasenia).IsRequired();
            modelBuilder.Entity<Usuario>().Property(u => u.Rol).IsRequired();
            modelBuilder.Entity<Usuario>().HasMany<Invitacion>(u => u.InvitacionesRealizadas).WithOne(i => i.Invitador).HasForeignKey(i => i.InvitadorId);


            modelBuilder.Entity<Invitacion>().HasKey(i => i.Id);
            modelBuilder.Entity<Invitacion>().Property(i => i.Rol).IsRequired();
            modelBuilder.Entity<Invitacion>().Property(i => i.Email).IsRequired();
            modelBuilder.Entity<Invitacion>().Property(i => i.Utilizada).IsRequired();
            modelBuilder.Entity<Invitacion>().Property(i => i.FechaVencimiento).IsRequired();

            modelBuilder.Entity<ApplicationKey>().HasKey(p => p.Id);
            modelBuilder.Entity<ApplicationKey>().Property(p => p.SessionId).IsRequired();
            modelBuilder.Entity<ApplicationKey>().Property(p => p.EmpresaId).IsRequired();
            modelBuilder.Entity<ApplicationKey>().Property(p => p.UsuarioId).IsRequired();
            modelBuilder.Entity<ApplicationKey>().Property(p => p.Activa).IsRequired();
            modelBuilder.Entity<ApplicationKey>().Property(p => p.FechaCreacion).IsRequired();
            modelBuilder.Entity<ApplicationKey>().Property(p => p.ApplicationKeyStr).IsRequired();
        }
    }
}
