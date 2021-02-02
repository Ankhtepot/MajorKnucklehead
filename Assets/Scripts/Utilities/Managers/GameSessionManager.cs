using System.Collections;
using Actors.Enemies;
using DTOs;
using Enumerations;
using UnityEngine;

namespace Utilities.Managers
{
    public class GameSessionManager : MonoBehaviour
    {
#pragma warning disable 649
        [SerializeField] private float checkAllEnemiesKilledCooldown = 0.5f;
        [SerializeField] private GameManager gameManager;
        
        private int activeSpawnersCount;
        private GameSessionStatistics sessionStatistics;

        public GameSessionStatistics SessionStatistics => sessionStatistics;
#pragma warning restore 649

        private void Awake()
        {
            sessionStatistics = new GameSessionStatistics();
            EventBroker.OnSpawnerRegister += OnSpawnerRegistered;
            EventBroker.OnSpawnerUnregister += OnSpawnerUnregistered;
            EventBroker.OnSceneUnloaded += OnSceneUnloaded;
            EventBroker.OnScoreGained += OnScoreGained;
        }
        
        public void StartGameSession()
        {
            activeSpawnersCount = 0;
            EventBroker.TriggerOnScoreGained(0);
            gameManager.SceneLoader.LoadLevelByLevelNumber(1, OnFirstLevelLoaded);
        }

        private void OnSpawnerRegistered()
        {
            activeSpawnersCount++;
        }

        private void OnSpawnerUnregistered()
        {
            activeSpawnersCount--;

            if (activeSpawnersCount == 0)
            {
                StartCoroutine(CheckAllEnemiesKilledRoutine());
            }
        }

        private void OnSceneUnloaded()
        {
            activeSpawnersCount = 0;
            sessionStatistics = new GameSessionStatistics(); //TODO: add per level statistics in the future
            StopAllCoroutines();
        }

        private void OnScoreGained(int scoreGained)
        {
            sessionStatistics.score += scoreGained;
            EventBroker.TriggerOnScoreChanged(sessionStatistics.score);
        }

        private int _foundEnemies = 1;
        
        private IEnumerator CheckAllEnemiesKilledRoutine()
        {
            while (_foundEnemies > 0)
            {
                yield return new WaitForSeconds(checkAllEnemiesKilledCooldown);
                _foundEnemies = FindObjectsOfType<Enemy>().Length;
            }
            
            EventBroker.TriggerOnAllEnemiesInLevelKilled();
        }

        private void OnDisable()
        {
            EventBroker.OnSpawnerUnregister -= OnSpawnerRegistered;
            EventBroker.OnSpawnerUnregister -= OnSpawnerUnregistered;
            EventBroker.OnSceneUnloaded -= OnSceneUnloaded;
        }

        private void OnFirstLevelLoaded(AsyncOperation operation)
        {
            gameManager.ChangeGameState(GameState.Running);
            EventBroker.TriggerOnGameSessionStarted();
            EventBroker.TriggerOnSceneLoaded();
        }
    }
}