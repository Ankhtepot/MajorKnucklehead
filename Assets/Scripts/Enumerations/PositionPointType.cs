using System;

namespace Enumerations
{
    [Flags]
    public enum PositionPointType
    {
        Ship = 1,
        MotherShipAtPlayer = 2,
        MotherShipFromPortal = 4
    }
}