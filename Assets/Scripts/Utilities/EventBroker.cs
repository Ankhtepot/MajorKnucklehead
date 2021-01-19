using System;

namespace Utilities
{
    public static class EventBroker
    {
        // Events

        public static event Action OnGameSessionStarted;

        // Callers

        public static void TriggerOnGameSessionStarted()
        {
            OnGameSessionStarted?.Invoke();
        }
    }
}