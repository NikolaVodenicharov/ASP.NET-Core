using CameraBazaar2.Data.Constants;
using CameraBazaar2.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace CameraBazaar2.Web.Models.Cameras
{
    public class CameraViewModel
    {
        [Required]
        public CameraMake Make { get; set; }

        [Required]
        [MaxLength(CameraConstants.ModelMaxLength)]
        [Display(Name = "Model")]
        public string CameraModel { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [Range(CameraConstants.QuantityMin, CameraConstants.QuantityMax)]
        public int Quantity { get; set; }

        [Required]
        [Range(CameraConstants.MinShutterSpeedMin, CameraConstants.MinShutterSpeedMax)]
        [Display(Name = "Min Shutter Speed")]
        public int MinShutterSpeed { get; set; }

        [Required]
        [Range(CameraConstants.MaxShutterSpeedMin, CameraConstants.MaxShutterSpeedMax)]
        [Display(Name = "Max Shutter Speed")]
        public int MaxShutterSpeed { get; set; }

        [Required]
        [Range(CameraConstants.MinIsoMin, CameraConstants.MinIsoMax)]
        [Display(Name = "Min ISO")]
        public int MinIso { get; set; }

        [Required]
        [Range(CameraConstants.MaxIsoMin, CameraConstants.MaxIsoMax)]
        [Display(Name = "Max ISO")]
        public int MaxIso { get; set; }

        [Required]
        [Display(Name = "Full Frame")]
        public bool IsFullFrame { get; set; }

        [Required]
        [MaxLength(CameraConstants.VideoResolutionMaxSymbols)]
        [Display(Name = "Video Resolution")]
        public string VideoResolution { get; set; }

        [Required]
        [MaxLength(CameraConstants.DescriptionMax)]
        public string Description { get; set; }

        [Required]
        [MaxLength(CameraConstants.ImageUrlMax)]
        [Display(Name = "Image URL")]
        [RegularExpression(@"^http[s]?:[\/]{2}.+", ErrorMessage = @"URL must start with http:// or https://")]
        public string ImageUrl { get; set; }
    }
}
