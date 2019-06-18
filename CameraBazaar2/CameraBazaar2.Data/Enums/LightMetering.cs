using System;

namespace CameraBazaar2.Data.Enums
{
    [Flags]
    public enum LightMetering
    {
        Spot = 1,
        CenterWeighted = 2,
        Evaluative = 4
    }
}
