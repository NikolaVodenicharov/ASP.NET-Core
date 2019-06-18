using CameraBazaar2.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace CameraBazaar2.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder CreateDefaultRoles(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                CreateRole(Constants.Administrator, roleManager);
                CreateRole(Constants.LoggedUser, roleManager);
                CreateRole(Constants.GuestUser, roleManager);
            }

            return app;
        }
        private static void CreateRole(string roleName, RoleManager<IdentityRole> roleManager)
        {
            Task
                .Run(async () =>
                {
                    var isRoleExist = await roleManager.RoleExistsAsync(roleName);

                    if (!isRoleExist)
                    {
                        var role = new IdentityRole(roleName);
                        await roleManager.CreateAsync(role);
                    }
                })
                .Wait();
        }

        public static IApplicationBuilder CreateDefaultAdministrator(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                Task
                    .Run(async () =>
                    {
                        var user = await userManager.FindByNameAsync(Constants.Administrator);
                        if (user != null)
                        {
                            return;
                        }

                        var isRoleExist = await roleManager.RoleExistsAsync(Constants.Administrator);
                        if (!isRoleExist)
                        {
                            throw new TaskCanceledException($"Can not create {Constants.Administrator} because the role does not exist.");
                        }

                        user = new User
                        {
                            UserName = Constants.Administrator,
                            Email = $"{Constants.Administrator}@gmail.com",
                            PhoneNumber = "+123456123456"
                        };

                        var result = await userManager.CreateAsync(user, "admin123");
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, Constants.Administrator);
                        }
                    })
                    .Wait();
            }

            return app;
        }
    }
}
