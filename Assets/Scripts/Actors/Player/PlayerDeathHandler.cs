using UnityEngine;
using Utilities;
using Utilities.BaseClasses;

//Fireball Games * * * PetrZavodny.com

namespace Actors.Player
{
    public class PlayerDeathHandler : DeathHandlerBase
    {
#pragma warning disable 649
    
#pragma warning restore 649

        public override void HandleDeath()
        {
            Debug.Log("player died");
        }
    }
}
