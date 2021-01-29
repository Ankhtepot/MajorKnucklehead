using Utilities;
using Utilities.BaseClasses;
using Utilities.ObjectPool;

//Fireball Games * * * PetrZavodny.com

namespace Actors.Enemies
{
    public class EnemyDeathHandler : DeathHandlerBase, IPoolNeedy
    {
#pragma warning disable 649
#pragma warning restore 649

        public override void HandleDeath()
        {
            ReturnToPool();
        }
    
        private void ReturnToPool()
        {
            pool.ReturnToPool(gameObject);
        }

        public ObjectPool pool { get; set; }
    }
}
