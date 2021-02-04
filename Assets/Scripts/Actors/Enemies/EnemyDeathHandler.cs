using Utilities;
using Utilities.BaseClasses;
using Utilities.ObjectPool;

//Fireball Games * * * PetrZavodny.com

namespace Actors.Enemies
{
    public class EnemyDeathHandler : DeathHandlerBase, IPoolNeedy
    {
#pragma warning disable 649
        public int scoreValue;
#pragma warning restore 649

        private void OnEnable()
        {
            EventBroker.OnGameSessionStopped += HandleDeath;
        }

        public override void HandleDeath()
        {
            EventBroker.TriggerOnScoreGained(scoreValue);
            ReturnToPool();
        }
    
        protected virtual void ReturnToPool()
        {
            pool.ReturnToPool(gameObject);
        }

        private void OnDisable()
        {
            EventBroker.OnGameSessionStopped -= HandleDeath;
        }

        public ObjectPool pool { get; set; }
    }
}
