using CameraBazaar2.Data;
using CameraBazaar2.Data.Enums;
using CameraBazaar2.Data.Models;
using CameraBazaar2.Services.Interfaces;
using CameraBazaar2.Services.Models.Cameras;
using System.Collections.Generic;
using System.Linq;

namespace CameraBazaar2.Services.Implementations
{
    public class CameraService : ICameraService
    {
        private CameraBazaarDbContext db;

        public CameraService(CameraBazaarDbContext db)
        {
            this.db = db;
        }

        public void Add(
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
            IEnumerable<LightMetering> lightMeterings, 
            string description, 
            string imageUrl,
            string userId)
        {
            var camera = new Camera
            {
                Make = make,
                Model = model,
                Price = price,
                Quantity = quantity,
                MinShutterSpeed = minShutterSpeed,
                MaxShutterSpeed = maxShutterSpeed,
                MinIso = minIso,
                MaxIso = maxIso,
                IsFullFrame = isFullFrame,
                VideoResolution = videoResolution,
                LightMetering = (LightMetering)lightMeterings.Cast<int>().Sum(),
                Description = description,
                ImageUrl = imageUrl,
                UserId = userId
            };

            this.db.Cameras.Add(camera);
            this.db.SaveChanges();
        }

        public List<CameraSummaryServiceModel> All()
        {
            var query = this.db.Cameras;
            return this.AllCameraSummary(query);
        }

        public List<CameraSummaryServiceModel> UserCameras(string userId)
        {
            var query = this.db
                .Cameras
                .Where(c => c.UserId == userId);

            return this.AllCameraSummary(query);
        }

        private List<CameraSummaryServiceModel> AllCameraSummary(IQueryable<Camera> query)
        {
            return query
                .Select(c => this.GetCameraSummary(c))
                .ToList();
        }
        private CameraSummaryServiceModel GetCameraSummary(Camera camera)
        {
            return new CameraSummaryServiceModel
            {
                Id = camera.Id,
                Make = camera.Make,
                Model = camera.Model,
                Price = camera.Price,
                IsInStock = camera.Quantity > 0,
                ImageUrl = camera.ImageUrl
            };
        }

        public CameraWithUserReferenceServiceModel GetCameraWithUser(int id)
        {
            var isCameraExist = this.db.Cameras.Any(c => c.Id == id);
            if (!isCameraExist)
            {
                return null;
            }

            var model = this.db
                .Cameras
                .Where(c => c.Id == id)
                .Select(c => new CameraWithUserReferenceServiceModel
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    Price = c.Price,
                    Quantity = c.Quantity,
                    MinShutterSpeed = c.MinShutterSpeed,
                    MaxShutterSpeed = c.MaxShutterSpeed,
                    MinIso = c.MinIso,
                    MaxIso = c.MaxIso,
                    IsFullFrame = c.IsFullFrame,
                    VideoResolution = c.VideoResolution,
                    LightMetering = c.LightMetering,
                    Description = c.Description,
                    ImageUrl = c.ImageUrl,
                    UserId = c.UserId,
                    Username = c.User.UserName
                })
                .First();

            return model;
        }

        public Camera GetCamera (int id)
        {
            return this.db.Cameras.Find(id);
        }

        public void Edit(
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
             IEnumerable<LightMetering> lightMeterings,
             string description,
             string imageUrl)
        {
            var cam = this.db.Cameras.Where(c => c.Id == id).FirstOrDefault();
            if (cam == null)
            {
                return;
            }

            cam.Make = make;
            cam.Model = model;
            cam.Price = price;
            cam.Quantity = quantity;
            cam.MinShutterSpeed = minShutterSpeed;
            cam.MaxShutterSpeed = maxShutterSpeed;
            cam.MinIso = minIso;
            cam.MaxIso = maxIso;
            cam.IsFullFrame = isFullFrame;
            cam.VideoResolution = videoResolution;
            cam.LightMetering = (LightMetering)lightMeterings.Cast<int>().Sum();
            cam.Description = description;
            cam.ImageUrl = imageUrl;

            this.db.SaveChanges();
        }
    }
}
