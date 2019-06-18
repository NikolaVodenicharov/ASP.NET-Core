using CameraBazaar2.Data.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CameraBazaar2.Web.Models.Cameras
{
    public class EditCameraViewModel : AddCameraViewModel
    {
        public int Id { get; set; }

        public LightMetering LightMetering { get; set; }

    }
}
