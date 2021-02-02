using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DTOs;
using Enumerations;
using Interface;
using ScriptableObjects;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities.Managers;

//Fireball Games * * * PetrZavodny.com

namespace Actors.Enemies
{
    public class MotherShipSpawner : SpawnerMono
    {
#pragma warning disable 649
        [SerializeField] private List<WaveConfiguration> EnemySpawnSequence;

        [Header("Assignables")]
        [SerializeField] private MotherShipDeathHandler deathHandler;

        private List<WaveConfiguration> enemyPool;
        private int _currentWaveConfigurationIndex;
#pragma warning restore 649
    
        public override void InitializeMoving(PositionPointsManager positionManager)
        {
            positionManager.RequestFreePositionWhenAvailable(this, PositionPointType.MotherShipAtPlayer);
        }

        public override void FreePositionAvailable(PositionPoint targetPosition)
        {
            var mover = GetComponent<MoverToPosition>();
            mover.StartMovingToPosition(targetPosition);
        }

        protected override void Awake()
        {
            base.Awake();
            
            deathHandler.pool = pool;
            enemyPool = new List<WaveConfiguration>();
        }

        protected override void OnGameSessionStarted()
        {
            enemyPool = EnemySpawnSequence;
        
            if (enemyPool.Any())
            {
                StartCoroutine(enemyPool[0].spawnInSequence 
                    ? SpawnEnemySequence(enemyPool[0]) 
                    : SpawnEnemySequenceRandom(enemyPool[0]));
            }
        }

        private IEnumerator SpawnEnemySequenceRandom(WaveConfiguration waveConfiguration)
        {
            //TODO: write this method
            yield return null;
        }

        private IEnumerator SpawnEnemySequence(WaveConfiguration waveConfiguration)
        {
            yield return new WaitForSeconds(waveConfiguration.initialDelay);

            foreach (var sequence in waveConfiguration.EnemiesToSpawn)
            {
                var enemyToSpawn = sequence.enemyPrefab.gameObject;
                for (int i = 0; i < sequence.spawnTimes; i++)
                {
                    var spawnedEnemy = pool.GetFromPool(enemyToSpawn, spawnPoint.position, quaternion.identity);
                    spawnedEnemy.GetComponent<Enemy>().InitializeMoving(positionsManager);
                    yield return new WaitForSeconds(waveConfiguration.timeBetweenSpawns);
                }
            }

            if (enemyPool.Count - 1 > _currentWaveConfigurationIndex)
            {
                _currentWaveConfigurationIndex += 1;
                StartCoroutine(SpawnEnemySequence(enemyPool[_currentWaveConfigurationIndex]));    
            }
            else
            {
                TriggerMotherShipMove();
            }

            yield return null;
        }

        private void TriggerMotherShipMove()
        {
            print("Mother Ship starts approaching the player");
            InitializeMoving(positionsManager);
        }
    }
}
