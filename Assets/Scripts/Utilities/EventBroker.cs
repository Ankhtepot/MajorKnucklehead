﻿using Enumerations;
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
        public static UnityAction OnSpawnerRegister;
        public static UnityAction OnSpawnerUnregister;
        public static UnityAction OnWinConditionMet;
        public static UnityAction OnSceneLoaded;
        public static UnityAction OnSceneUnloaded;
        public static UnityAction<GameState, GameState> OnGameStateChanged;
        public static UnityAction<int> OnScoreGained;
        public static UnityAction<int> OnScoreChanged;
        public static UnityAction<AudioClipPurpose> OnPlayAudioRequested;

        // Callers

        public static void TriggerOnGameSessionStarted() => OnGameSessionStarted?.Invoke();
        public static void TriggerOnGameSessionStartRequested() => OnGameSessionStartRequested?.Invoke();
        public static void TriggerOnGameSessionStopped() => OnGameSessionStopped?.Invoke();
        public static void TriggerOnGameSessionStopRequested() => OnGameSessionStopRequested?.Invoke();
        public static void TriggerOnSpawnerRegistering() => OnSpawnerRegister?.Invoke();
        public static void TriggerOnSpawnerUnregister() => OnSpawnerUnregister?.Invoke();
        public static void TriggerOnWinConditionMet() => OnWinConditionMet?.Invoke();
        public static void TriggerOnSceneLoaded() => OnSceneLoaded?.Invoke();
        public static void TriggerOnSceneUnloaded() => OnSceneUnloaded?.Invoke();
        public static void TriggerOnGameStateChanged(GameState previousGameState, GameState currentGameState) 
            => OnGameStateChanged?.Invoke(previousGameState, currentGameState);
        public static void TriggerOnScoreGained(int score) => OnScoreGained?.Invoke(score);
        public static void TriggerOnScoreChanged(int sessionStatisticsScore) => OnScoreChanged?.Invoke(sessionStatisticsScore);
        public static void TriggerOnPlayAudioRequested(AudioClipPurpose purpose) => OnPlayAudioRequested?.Invoke(purpose);
    }
}