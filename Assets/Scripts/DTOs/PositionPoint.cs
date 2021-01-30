using Enumerations;
using UnityEngine;
using Utilities;

//Fireball Games * * * PetrZavodny.com

namespace DTOs
{
    public class PositionPoint : ExtendedMono
    {
#pragma warning disable 649
        public PositionPointType positionType;
        public bool occupied;

        public Vector3 Position => transform.position;
#pragma warning restore 649


    }
}
