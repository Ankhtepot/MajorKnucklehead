using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Markup;
using DTOs;
using Enumerations;
using Interface;
using ScriptableObjects;
using Unity.Mathematics;
using UnityEngine;
using Utilities.Managers;

//Fireball Games * * * PetrZavodny.com

namespace Actors.Enemies
{
    public class MotherShipSpawner : MovingSpawnerMono
    {
#pragma warning disable 649
        [Header("Assignables")]
        [SerializeField] private MotherShipDeathHandler deathHandler;
        
        private int _currentWaveConfigurationIndex;
        private bool _isOnSpawningPosition;
#pragma warning restore 649
    
        protected override void Awake()
        {
            base.Awake();
            
            deathHandler.pool = pool;
            deathHandler.scoreValue = scoreValue;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _isOnSpawningPosition = false;
            TriggerMotherShipMove();
        }

        public override void InitializeMoving(PositionPointsManager positionManager)
        {
            if (_isOnSpawningPosition)
            {
                positionManager.RequestFreePositionWhenAvailable(this, PositionPointType.MotherShipAtPlayer);
                return;
            }
            
            positionManager.RequestFreePositionWhenAvailable(this, PositionPointType.MotherShipFromPortal);
        }

        public override void FreePositionAvailable(PositionPoint targetPosition)
        {
            if (!_isOnSpawningPosition)
            {
                mover.OnPositionReached.AddListener(OnSpawningPositionReached);
            }
            else
            {
                mover.OnPositionReached.RemoveListener(OnSpawningPositionReached);  // MotherShip will move now to player, no more enemy spawning
            }
            
            mover.StartMovingToPosition(targetPosition);
        }

        private void OnSpawningPositionReached()
        {
            _isOnSpawningPosition = true;
            base.StartSpawningWaves();
        }

        protected override IEnumerator SpawnEnemySequenceRandom(WaveConfiguration waveConfiguration)
        {
            yield return base.SpawnEnemySequenceRandom(waveConfiguration);
        }

        protected override IEnumerator SpawnEnemySequence(WaveConfiguration waveConfiguration)
        {
            yield return base.SpawnEnemySequence(waveConfiguration);

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
            print("Mother Ship starts approaching the " + (!_isOnSpawningPosition ? "spawning position" : "player") + ".");
            InitializeMoving(positionsManager);
        }
    }
}
