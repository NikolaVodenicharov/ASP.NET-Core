using CameraBazaar2.Data.Enums;
using CameraBazaar2.Data.Models;
using CameraBazaar2.Services.Models.Cameras;
using System.Collections.Generic;

namespace CameraBazaar2.Services.Interfaces
{
    public interface ICameraService
    {
        void Add(
            CameraMake make,
            string model,
            decimal price,
            int quantity,
            int minShutterSpeed,
            int maxShutterSpeed,
            int minIso,
            int maxIso,
            bool isFullFrame,
            string videoResolution,
            IEnumerable<LightMetering> lightMetering,
            string description,
            string imageUrl,
            string userId);


        List<CameraSummaryServiceModel> All();

        List<CameraSummaryServiceModel> UserCameras(string userId);

        CameraWithUserReferenceServiceModel GetCameraWithUser(int id);

        Camera GetCamera(int id);

        void Edit(
             int id,
             CameraMake make,
             string model,
             decimal price,
             int quantity,
             int minShutterSpeed,
             int maxShutterSpeed,
             int minIso,
             int maxIso,
             bool isFullFrame,
             string videoResolution,
             IEnumerable<LightMetering> lightMetering,
             string description,
             string imageUrl);
    }

}
