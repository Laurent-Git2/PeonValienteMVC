using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PeonValienteMVC
{
    public class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            // Crear roles predeterminados
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            await CrearRolesAsync(roleManager);

            // Crear administrador predeterminado
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            await CrearAdminAsync(userManager);
        }

        private static async Task CrearRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            // Rol Administrador
            string nombreRol = "Administrador";

            var yaExiste = await roleManager.RoleExistsAsync(nombreRol);

            if (!yaExiste)
                await roleManager.CreateAsync(new IdentityRole(nombreRol));

            // Rol Usuario
            nombreRol = "Usuario";

            yaExiste = await roleManager.RoleExistsAsync(nombreRol);

            if (!yaExiste)
                await roleManager.CreateAsync(new IdentityRole(nombreRol));
        }

        private static async Task CrearAdminAsync(UserManager<IdentityUser> userManager)
        {
            // Buscar si ya existe el administrador
            var testAdmin = userManager.Users
                .Where(x => x.UserName == "admin@peonvaliente.com")
                .SingleOrDefault();

            if (testAdmin != null)
                return;

            // Crear administrador
            testAdmin = new IdentityUser
            {
                UserName = "admin@peonvaliente.com",
                Email = "admin@peonvaliente.com",
                EmailConfirmed = true
            };

            string admPasswd = "Admin-123";

            var userResult = await userManager.CreateAsync(testAdmin, admPasswd);

            // Asignar rol Administrador
            if (userResult.Succeeded)
            {
                await userManager.AddToRoleAsync(testAdmin, "Administrador");
            }
        }
    }
}
