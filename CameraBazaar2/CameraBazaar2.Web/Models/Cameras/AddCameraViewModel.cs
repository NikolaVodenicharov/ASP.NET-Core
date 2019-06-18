using CameraBazaar2.Data.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CameraBazaar2.Web.Models.Cameras
{
    public class AddCameraViewModel : CameraViewModel
    {
        public SelectList Makes { get; set; } = new SelectList(Enum.GetValues(typeof(CameraMake)));

        [Required]
        [Display(Name = "Light Metering")]
        public List<LightMetering> LightMeterings { get; set; } = new List<LightMetering>();
    }
}
