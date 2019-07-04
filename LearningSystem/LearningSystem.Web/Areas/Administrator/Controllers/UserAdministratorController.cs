using LearningSystem.Data.Models;
using LearningSystem.Services.Interfaces;
using LearningSystem.Web.Areas.Administrator.Models.Users;
using LearningSystem.Web.Infrastructure.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles = RoleConstants.Administrator)]
    [Route("UserAdministrator")]
    public class UserAdministratorController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IUserService userService;

        public UserAdministratorController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IUserService userService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.userService = userService;
        }

        [Route(nameof(AllUsersListing))]
        public IActionResult AllUsersListing()
        {
            var model = this.userService.AllByPages();

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