using System;
using Enumerations;
using UnityEngine.Events;

namespace Utilities
{
    public static class EventBroker
    {
        // Events

        public static UnityAction OnGameSessionStarted;
        public static UnityAction OnGameSessionStartRequested;
        public static UnityAction OnGameSessionStopped;
        public static UnityAction OnGameSessionStopRequested;
        public static UnityAction<GameState, GameState> OnGameStateChanged;

        // Callers

        public static void TriggerOnGameSessionStarted() => OnGameSessionStarted?.Invoke();

        public static void TriggerOnGameSessionStartRequested() => OnGameSessionStartRequested?.Invoke();

        public static void TriggerOnGameSessionStopped() => OnGameSessionStopped?.Invoke();

        public static void TriggerOnGameSessionStopRequested() => OnGameSessionStopRequested?.Invoke();

        public static void TriggerOnGameStateChanged(GameState previousGameState, GameState currentGameState) 
            => OnGameStateChanged?.Invoke(previousGameState, currentGameState);
    }
}