using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DTOs;
using ScriptableObjects;
using UnityEngine;
using Utilities;

//Fireball Games * * * PetrZavodny.com

public class MotherShipSpawner : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private List<WaveConfiguration> EnemySpawnSequence;

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
        yield return null;
    }

    private IEnumerator SpawnEnemySequence(WaveConfiguration waveConfiguration)
    {
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
