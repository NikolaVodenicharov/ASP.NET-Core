using CameraBazaar2.Services.Models.Cameras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CameraBazaar2.Web.Models.Cameras
{
    public class UserWithCamerasSummary
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsAuthorizeToAlterCameras { get; set; }
        public List<CameraSummaryServiceModel> CamerasSummary { get; set; } = new List<CameraSummaryServiceModel>();

        public int CamerasInStock => this.CamerasSummary.Where(c => c.IsInStock).Count();
        public int CamerasOutOfStock => this.CamerasSummary.Where(c => !c.IsInStock).Count();

    }
}
