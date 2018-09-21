namespace CameraBazaar.Data
{
    using System;

    public class DbConstants
    {
        public class CameraConst
        {
            public const int QuantityMin = 0;
            public const int QuantityMax = 100;

            public const int MinShutterSpeedMin = 1;
            public const int MinShutterSpeedMax = 30;

            public const int MaxShutterSpeedMin = 2000;
            public const int MaxShutterSpeedMax = 8000;

            public const int MinIsoMin = 50;
            public const int MinIsoMax = 100;

            public const int MaxIsoMin = 200;
            public const int MaxIsoMax = 409600;

            public const int VideoResolutionMaxLength = 15;

            public const int DescriptionMaxLength = 6000;
        }
    }
}
