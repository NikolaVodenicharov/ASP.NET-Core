using CameraBazaar2.Data.Enums;
using CameraBazaar2.Data.Models;
using CameraBazaar2.Services.Interfaces;
using CameraBazaar2.Web.Infrastructure.Attributes;
using CameraBazaar2.Web.Models.Cameras;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CameraBazaar2.Web.Controllers
{
    [Route("Cameras")]
    public class CamerasController : Controller
    {
        private readonly ICameraService cameraService;
        private UserManager<User> userManager;

        public CamerasController(ICameraService cameraService, UserManager<User> userManager)
        {
            this.cameraService = cameraService;
            this.userManager = userManager;
        }

        [Authorize]
        [Route(nameof(Add))]
        public IActionResult Add()
        {
            return View(new AddCameraViewModel());
        }

        [Authorize]
        [HttpPost]
        [Route(nameof(Add))]
        public IActionResult Add(AddCameraViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.cameraService.Add(
                model.Make,
                model.CameraModel,
                model.Price,
                model.Quantity,
                model.MinShutterSpeed,
                model.MaxShutterSpeed,
                model.MinIso,
                model.MaxIso,
                model.IsFullFrame,
                model.VideoResolution,
                model.LightMeterings,
                model.Description,
                model.ImageUrl,
                userManager.GetUserId(User));

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [Route(nameof(All))]
        public IActionResult All()
        {
            var model = this.cameraService.All();
            return View(model);
        }

        [Route(nameof(Details) + "/{id}")]
        public IActionResult Details(int id)
        {
            var model = this.cameraService.GetCameraWithUser(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [Route(nameof(UserCamerasSummary) + "/{id}")]
        public async Task<IActionResult> UserCamerasSummary(string id)
        {
            var loggedUser = await userManager.GetUserAsync(User);
            var user = await userManager.FindByIdAsync(id);

            var model = new UserWithCamerasSummary
            {
                Id = id,
                Username = user.UserName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                IsAuthorizeToAlterCameras = loggedUser.Id.Equals(id),
                CamerasSummary = this.cameraService.All()
            };

            return View(model);
        }

        [Route(nameof(Edit) + "/{id}")]
        public IActionResult Edit (int id)
        {
            var camera = this.cameraService.GetCamera(id);
            if (camera == null)
            {
                return NotFound();
            }

            var model = new EditCameraViewModel
            {
                Id = id,
                Make = camera.Make,
                CameraModel = camera.Model,
                Price = camera.Price,
                Quantity = camera.Quantity,
                MinShutterSpeed = camera.MinShutterSpeed,
                MaxShutterSpeed = camera.MaxShutterSpeed,
                MinIso = camera.MinIso,
                MaxIso = camera.MaxIso,
                IsFullFrame = camera.IsFullFrame,
                LightMetering = camera.LightMetering,
                VideoResolution = camera.VideoResolution,
                Description = camera.Description,
                ImageUrl = camera.ImageUrl,
            };

            return View(model);
        }

        [Route(nameof(Edit) + "/{id}")]
        [HttpPost]
        public IActionResult Edit(EditCameraViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.cameraService.Edit(
                model.Id,
                model.Make,
                model.CameraModel,
                model.Price,
                model.Quantity,
                model.MinShutterSpeed,
                model.MaxShutterSpeed,
                model.MinIso,
                model.MaxIso,
                model.IsFullFrame,
                model.VideoResolution,
                model.LightMeterings,
                model.Description,
                model.ImageUrl);

            return RedirectToAction(nameof(All));
        }
    }
}