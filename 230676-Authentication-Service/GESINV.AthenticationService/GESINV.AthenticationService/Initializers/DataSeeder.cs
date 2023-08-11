using GESINV.AthenticationService.Dominio;
using GESINV.AthenticationService.PersistanceAccess;
using Microsoft.EntityFrameworkCore;

namespace GESINV.AthenticationService.Initializers
{
    public static class DataSeeder
    {
        public static WebApplication SeedDB(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                using var contexto = scope.ServiceProvider.GetRequiredService<AuthenticationContext>();
                try
                {
                    contexto.Database.EnsureCreated();
                    AddEmpresaYUsuariosIniciales(contexto);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return app;
        }

        public static void AddEmpresaYUsuariosIniciales(DbContext context)
        {
            DbSet<Empresa> empresas = context.Set<Empresa>();
            Empresa? empresa = empresas.FirstOrDefault();

            if (empresa != null) return;

            Empresa empresaOrt = new Empresa()
            {
                Id = Guid.NewGuid(),
                Nombre = "ORT"
            };

            Usuario usuarioAdminOrt = new Usuario()
            {
                Id = Guid.NewGuid(),
                Nombre = "admin",
                Email = "admin@ort.com",
                Contrasenia = "Password1",
                Empresa = empresaOrt,
                Rol = Roles.RolAdministrador
            };
            Usuario usuarioEmpleadoOrt = new Usuario()
            {
                Id = Guid.NewGuid(),
                Nombre = "empleado",
                Email = "empleado@ort.com",
                Contrasenia = "Password1",
                Empresa = empresaOrt,
                Rol = Roles.RolEmpleado
            };

            DbSet<Usuario> usuarios = context.Set<Usuario>();

            empresas.Add(empresaOrt);
            usuarios.Add(usuarioAdminOrt);
            usuarios.Add(usuarioEmpleadoOrt);
            context.SaveChanges();
        }
    }
}

