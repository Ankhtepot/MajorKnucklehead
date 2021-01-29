

//Fireball Games * * * PetrZavodny.com

using UnityEngine;
using Utilities;
using Utilities.BaseClasses;

namespace Actors.Player
{
    public class PlayerHealth : HealthBase
    {
        protected override void SetDamagingTag()
        {
            DamagingTag = Strings.EnemyDamage;
        }
    }
}
