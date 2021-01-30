using UnityEngine;

namespace Utilities.Managers
{
    public class GameSessionManager : MonoBehaviour
    {
        private int activeSpawners;

        private void Awake()
        {
            EventBroker.OnSpawnerRegister += () => activeSpawners++;
            EventBroker.OnSpawnerFinished += OnSpawnerUnregistered;
        }

        private void OnSpawnerUnregistered()
        {
            activeSpawners--;

            if (activeSpawners == 0)
            {
                EventBroker.TriggerAllSpawnersFinished();
            }
        }
    }
}