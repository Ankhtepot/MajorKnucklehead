using Utilities;
using Utilities.BaseClasses;

//Fireball Games * * * PetrZavodny.com

namespace Actors.Enemies
{
    public class EnemyHealth : HealthBase
    {
        protected override void SetDamagingTag()
        {
            DamagingTag = Strings.PlayerDamage;
        }
    }
}
