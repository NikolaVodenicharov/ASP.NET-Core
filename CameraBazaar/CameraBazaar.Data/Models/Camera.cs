namespace CameraBazaar.Data.Models
{
    using CameraBazaar.Data.Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Camera
    {
        public int Id { get; set; }

        [Required]
        public MakeEnum Make { get; set; }


        public string Model { get; set; }

        public decimal Price { get; set; }

        [Range(DbConstants.CameraConst.QuantityMin, DbConstants.CameraConst.QuantityMax)]
        public int Quantity { get; set; }

        [Range(DbConstants.CameraConst.MinShutterSpeedMin, DbConstants.CameraConst.MinShutterSpeedMax)]
        public int MinShutterSpeed { get; set; }

        [Range(DbConstants.CameraConst.MaxShutterSpeedMin, DbConstants.CameraConst.MaxShutterSpeedMax)]
        public int MaxShutterSpeed { get; set; }

        [Range(DbConstants.CameraConst.MinIsoMin, DbConstants.CameraConst.MinIsoMax)]
        public int MinISO { get; set; }

        [Range(DbConstants.CameraConst.MaxIsoMin, DbConstants.CameraConst.MaxIsoMax)]
        public int MaxISO { get; set; }

        public FullFrame IsFullFrame { get; set; }

        [MaxLength(DbConstants.CameraConst.VideoResolutionMaxLength)]
        public string VideoResolution  { get; set; }

        public IEnumerable<LightMetering> LightMeterings { get; set; } = new List<LightMetering>();

        [MaxLength(DbConstants.CameraConst.DescriptionMaxLength)]
        public string Description { get; set; }

        [Url]
        public string ImageURL  { get; set; }
    }
}
