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
#pragma warning restore 649

    private void Awake()
    {
        initialize();
    }

    private void OnGameSessionStarted()
    {
        if (EnemySpawnSequence.Any())
        {
            
        }
    }

    private void OnDisable()
    {
        EventBroker.OnGameSessionStarted -= OnGameSessionStarted;
    }

    private void initialize()
    {
        EventBroker.OnGameSessionStarted += OnGameSessionStarted;
        spawnPoint = spawnPoint.transform;
    }
}
