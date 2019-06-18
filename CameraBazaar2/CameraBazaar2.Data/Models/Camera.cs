namespace CameraBazaar2.Data.Models
{
    using CameraBazaar2.Data.Constants;
    using CameraBazaar2.Data.Enums;
    using System.ComponentModel.DataAnnotations;

    public class Camera
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public CameraMake Make { get; set; }

        [Required]
        [MaxLength(CameraConstants.ModelMaxLength)]
        public string Model { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [Range(CameraConstants.QuantityMin, CameraConstants.QuantityMax)]
        public int Quantity { get; set; }

        [Required]
        [Range(CameraConstants.MinShutterSpeedMin, CameraConstants.MinShutterSpeedMax)]
        public int MinShutterSpeed { get; set; }

        [Required]
        [Range(CameraConstants.MaxShutterSpeedMin, CameraConstants.MaxShutterSpeedMax)]
        public int MaxShutterSpeed { get; set; }

        [Required]
        [Range(CameraConstants.MinIsoMin, CameraConstants.MinIsoMax)]
        public int MinIso { get; set; }

        [Required]
        [Range(CameraConstants.MaxIsoMin, CameraConstants.MaxIsoMax)]
        public int MaxIso { get; set; }

        [Required]
        public bool IsFullFrame { get; set; }

        [Required]
        [MaxLength(CameraConstants.VideoResolutionMaxSymbols)]
        public string VideoResolution { get; set; }

        [Required]
        public LightMetering LightMetering { get; set; }

        [Required]
        [MaxLength(CameraConstants.DescriptionMax)]
        public string Description { get; set; }

        [Required]
        [MaxLength(CameraConstants.ImageUrlMax)]
        public string ImageUrl { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
