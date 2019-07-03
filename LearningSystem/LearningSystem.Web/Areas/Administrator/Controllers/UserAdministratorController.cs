using LearningSystem.Data.Models;
using LearningSystem.Services.Interfaces;
using LearningSystem.Web.Areas.Administrator.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Route("UserAdministrator")]
    public class UserAdministratorController : Controller
    {
        private UserManager<User> userManager;
        private RoleManager<IdentityRole> roleManager;
        private IUserService userService;

        public UserAdministratorController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IUserService userService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.userService = userService;
        }

        [Route(nameof(AllUsersListing))]
        public IActionResult AllUsersListing()
        {
            var model = this.userService.AllUsersListing();

            return View(model);
        }

        [Route(nameof(Details) + "/{id}")]
        public async Task<IActionResult> Details (string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new UserRoleViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                CurrentRoles = await this.userManager.GetRolesAsync(user),
                ChooseRoles = this.roleManager.Roles.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
            };

            return View(model);
        }

        [HttpPost]
        [Route(nameof(SetRole))]
        public async Task<IActionResult> SetRole(UserRoleViewModel model)
        {
            var user = await this.userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return NotFound();
            }

            var isRoleExist = await this.roleManager.RoleExistsAsync(model.ChosedRole);
            if (!isRoleExist)
            {
                return NotFound();
            }

            await this.userManager.AddToRoleAsync(user, model.ChosedRole);

            return RedirectToAction(nameof(AllUsersListing));
        }
    }
}