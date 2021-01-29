using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Actors.Enemies;
using DTOs;
using ScriptableObjects;
using Unity.Mathematics;
using UnityEngine;
using Utilities;
using Utilities.ObjectPool;

//Fireball Games * * * PetrZavodny.com

public class MotherShipSpawner : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private List<WaveConfiguration> EnemySpawnSequence;

    [Header("Assignables")] [SerializeField]
    private PositionPointsManager positionsManager;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private ObjectPool pool;

    private List<WaveConfiguration> enemyPool;
    private int currentWaveConfigurationIndex = 0;
#pragma warning restore 649

    private void Awake()
    {
        initialize();
    }

    private void OnGameSessionStarted()
    {
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
        
        yield return null;
    }

    private void OnDisable()
    {
        EventBroker.OnGameSessionStarted -= OnGameSessionStarted;
    }

    private void initialize()
    {
        EventBroker.OnGameSessionStarted += OnGameSessionStarted;
        spawnPoint = spawnPoint.transform;
        
        enemyPool = new List<WaveConfiguration>();
        enemyPool = EnemySpawnSequence;
    }
}
