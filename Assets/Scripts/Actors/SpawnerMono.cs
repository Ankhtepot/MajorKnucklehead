using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Actors.Enemies;
using DTOs;
using Interface;
using ScriptableObjects;
using Unity.Mathematics;
using UnityEngine;
using Utilities;
using Utilities.BaseClasses;
using Utilities.Managers;
using Utilities.ObjectPool;

namespace Actors
{
    public abstract class SpawnerMono : EnemyBase
    {
        [SerializeField] protected List<WaveConfiguration> EnemySpawnSequence;
        [SerializeField] protected Transform spawnPoint;
        protected ObjectPool pool;
        protected PositionPointsManager positionsManager;
        protected List<WaveConfiguration> enemyPool;

        protected virtual void Awake()
        {
            pool = GameManager.Pool;
        }

        protected virtual void OnEnable()
        {
            positionsManager = GameManager.PositionManager;
            EventBroker.TriggerOnSpawnerRegistering();
            enemyPool = EnemySpawnSequence;
        }

        protected virtual void StartSpawningWaves()
        {
            if (enemyPool.Any())
            {
                StartCoroutine(enemyPool[0].spawnInSequence 
                    ? SpawnEnemySequence(enemyPool[0]) 
                    : SpawnEnemySequenceRandom(enemyPool[0]));
            }
        }

        protected virtual IEnumerator SpawnEnemySequenceRandom(WaveConfiguration waveConfiguration)
        {
            //TODO: write this method
            yield return null;
        }

        protected virtual IEnumerator SpawnEnemySequence(WaveConfiguration waveConfiguration)
        {
            yield return new WaitForSeconds(waveConfiguration.initialDelay);

            foreach (var sequence in waveConfiguration.EnemiesToSpawn)
            {
                var enemyToSpawn = sequence.enemyPrefab.gameObject;
                for (int i = 0; i < sequence.spawnTimes; i++)
                {
                    var spawnedEnemy = pool.GetFromPool(enemyToSpawn, spawnPoint.position, quaternion.identity);
                    
                    var enemyComponent = spawnedEnemy.GetComponent<Enemy>();
                    if (enemyComponent)
                    {
                        enemyComponent.InitializeMoving(positionsManager);
                    }
                    
                    yield return new WaitForSeconds(waveConfiguration.timeBetweenSpawns);
                }
            }
        }
    }
}