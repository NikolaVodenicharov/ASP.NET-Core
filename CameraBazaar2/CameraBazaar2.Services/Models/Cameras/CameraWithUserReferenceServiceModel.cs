using CameraBazaar2.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CameraBazaar2.Services.Models.Cameras
{
    public class CameraWithUserReferenceServiceModel
    {
        public int Id { get; set; }
        public CameraMake Make { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int MinShutterSpeed { get; set; }
        public int MaxShutterSpeed { get; set; }
        public int MinIso { get; set; }
        public int MaxIso { get; set; }
        public bool IsFullFrame { get; set; }
        public string VideoResolution { get; set; }
        public LightMetering LightMetering { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public string UserId { get; set; }
        public string Username { get; set; }
    }
}
