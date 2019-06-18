using CameraBazaar2.Data;
using CameraBazaar2.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CameraBazaar2.Web.Infrastructure
{
    public class Seeder
    {
        private UserManager<User> userManager;
        private RoleManager<IdentityRole> roleManager;

        public Seeder (UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public void CreateRoles()
        {
            var rolesTasks = new Task[]
            {
                this.CreateRoleAsync(Constants.Administrator),
                this.CreateRoleAsync(Constants.LoggedUser),
                this.CreateRoleAsync(Constants.GuestUser)
            };
            Task.WaitAll(rolesTasks);
        }
        private async Task CreateRoleAsync(string roleName)
        {
            var isRoleExist = await this.roleManager.RoleExistsAsync(roleName);

            if (!isRoleExist)
            {
                var role = new IdentityRole(roleName);
                await this.roleManager.CreateAsync(role);
            }
        }

        public void AddDefaultAdministrator()
        {
            Task
                .Run(async () =>
                {
                    var user = await this.userManager.FindByNameAsync(Constants.Administrator);
                    if (user != null)
                    {
                        return;
                    }

                    user = new User
                    {
                        UserName = Constants.Administrator,
                        Email = $"{Constants.Administrator}@gmail.com",
                        PhoneNumber = "+123456123456"
                    };

                    var result = await this.userManager.CreateAsync(user, "admin123");
                    if (result.Succeeded)
                    {
                        await this.userManager.AddToRoleAsync(user, Constants.Administrator);
                    }
                })
                .Wait();
        }
    }
}
