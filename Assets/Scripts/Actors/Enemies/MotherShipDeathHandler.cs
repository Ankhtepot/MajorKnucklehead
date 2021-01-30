using Utilities;

namespace Actors.Enemies
{
    public class MotherShipDeathHandler : EnemyDeathHandler
    {
        protected override void ReturnToPool()
        {
            //TODO: try fix return to pool
        }
        
        public override void HandleDeath()
        {
            EventBroker.TriggerOnSpawnerUnregister();
            base.HandleDeath();
        }
    }
}