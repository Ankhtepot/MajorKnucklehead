using Utilities;

namespace Actors.Enemies
{
    public class MotherShipDeathHandler : EnemyDeathHandler
    {
        public override void HandleDeath()
        {
            EventBroker.TriggerOnSpawnerUnregister();
            base.HandleDeath();
        }
    }
}