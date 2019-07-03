namespace LearningSystem.Web.Infrastructure.Extensions
{
    using LearningSystem.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading.Tasks;
    using static Constants.Roles;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder CreateDefaultRoles(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                var roles = new string[]
                {
                    Administrator,
                    BlogAuthor,
                    Trainer,
                    Student,
                    Guest,
                };

                foreach (var role in roles)
                {
                    CreateRole(role, roleManager);
                }
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
                        var user = await userManager.FindByNameAsync(Administrator);
                        if (user != null)
                        {
                            return;
                        }

                        var isRoleExist = await roleManager.RoleExistsAsync(Administrator);
                        if (!isRoleExist)
                        {
                            throw new TaskCanceledException($"Can not create {Administrator} because the role does not exist.");
                        }

                        user = new User
                        {
                            Name = Administrator,
                            UserName = Administrator,
                            Email = $"{Administrator}@gmail.com",
                        };

                        var result = await userManager.CreateAsync(user, "admin123");
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, Administrator);
                        }
                    })
                    .Wait();
            }

            return app;
        }
    }
}
