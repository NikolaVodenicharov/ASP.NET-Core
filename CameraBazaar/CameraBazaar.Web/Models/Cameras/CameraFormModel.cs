namespace CameraBazaar.Web.Models.Cameras
{
    using CameraBazaar.Data;
    using CameraBazaar.Data.Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using CameraBazaar.Web.Common.Attrubutes;

    public class CameraFormModel
    {
        [Required]
        public MakeEnum Make { get; set; }

        [Required]
        [RegularExpression("[A-Z0-9_]+", ErrorMessage = "{0} can contain only uppercase letters, digits and dash.")]
        public string Model { get; set; }

        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "{0} must be number intiger or decimal with maximum 2 digits after floating pont.")]
        public decimal Price { get; set; }

        [Range(DbConstants.CameraConst.QuantityMin, DbConstants.CameraConst.QuantityMax)]
        public int Quantity { get; set; }

        [Range(DbConstants.CameraConst.MinShutterSpeedMin, DbConstants.CameraConst.MinShutterSpeedMax)]
        public int MinShutterSpeed { get; set; }

        [Range(DbConstants.CameraConst.MaxShutterSpeedMin, DbConstants.CameraConst.MaxShutterSpeedMax)]
        public int MaxShutterSpeed { get; set; }

        [Range(DbConstants.CameraConst.MinIsoMin, DbConstants.CameraConst.MinIsoMax)]
        public int MinISO { get; set; }

        [MaxIso(DbConstants.CameraConst.MaxIsoMin, DbConstants.CameraConst.MaxIsoMax)]
        public int MaxISO { get; set; }

        public FullFrame IsFullFrame { get; set; }

        [MaxLength(DbConstants.CameraConst.VideoResolutionMaxLength)]
        public string VideoResolution { get; set; }

        public IList<CkeckboxLightMeteringModel> CheckboxLightMeteringModels { get; set; } = new List<CkeckboxLightMeteringModel>();

        [MaxLength(DbConstants.CameraConst.DescriptionMaxLength)]
        public string Description { get; set; }

        [Url]
        public string ImageURL { get; set; }
    }
}
