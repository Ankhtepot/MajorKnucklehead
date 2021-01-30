﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Actors;
using Actors.Enemies;
using DTOs;
using Enumerations;
using Interface;
using ScriptableObjects;
using Unity.Mathematics;
using UnityEngine;
using Utilities;
using Utilities.Managers;
using Utilities.ObjectPool;

//Fireball Games * * * PetrZavodny.com

public class MotherShipSpawner : SpawnerMono, IMoveToPointSubscriber
{
#pragma warning disable 649
    [SerializeField] private List<WaveConfiguration> EnemySpawnSequence;

    [Header("Assignables")] [SerializeField]
    private PositionPointsManager positionsManager;
    [SerializeField] private Transform spawnPoint;

    private ObjectPool pool;
    private List<WaveConfiguration> enemyPool;
    private int _currentWaveConfigurationIndex = 0;
#pragma warning restore 649
    
    public void InitializeMoving(PositionPointsManager positionManager)
    {
        positionManager.RequestFreePositionWhenAvailable(this, PositionPointType.MotherShip);
    }

    public void FreePositionAvailable(PositionPoint targetPosition)
    {
        var mover = GetComponent<MoverToPosition>();
        mover.StartMovingToPosition(targetPosition);
    }

    private void Awake()
    {
        initialize();
    }

    private void OnGameSessionStarted()
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

    private void OnDisable()
    {
        EventBroker.OnGameSessionStarted -= OnGameSessionStarted;
    }

    private void initialize()
    {
        EventBroker.TriggerOnSpawnerRegistering();
        EventBroker.OnGameSessionStarted += OnGameSessionStarted;
        spawnPoint = spawnPoint.transform;
        GetComponent<MotherShipDeathHandler>().pool = pool;
        pool = GameManager.Pool;
        
        enemyPool = new List<WaveConfiguration>();
    }
}