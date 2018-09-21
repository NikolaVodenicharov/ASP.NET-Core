namespace CameraBazaar.Web.Controllers
{
    using CameraBazaar.Data.Enums;
    using CameraBazaar.Web.Models.Cameras;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;

    [Route("camera")]
    public class CameraController : Controller
    {
        [Route("add")]
        public IActionResult Add()
        {
            var cameraModel = new CameraFormModel();

            var lightMeteringModels = new List<CkeckboxLightMeteringModel>();
            foreach (LightMetering lightMetering in Enum.GetValues(typeof(LightMetering)))
            {
                lightMeteringModels.Add(new CkeckboxLightMeteringModel
                {
                    LightMetering = lightMetering,
                    IsCheck = false
                });
            }

            cameraModel.CheckboxLightMeteringModels = lightMeteringModels;

            return View(cameraModel);
        }

        [Route("add")]
        [HttpPost]
        public IActionResult Add(CameraFormModel cameraModel)
        {
            if (!ModelState.IsValid)
            {
                return View(cameraModel);
            }

            return null;
        }
    }
}
