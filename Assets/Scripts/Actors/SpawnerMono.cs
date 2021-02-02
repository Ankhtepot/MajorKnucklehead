using System;
using System.Collections.Generic;
using DTOs;
using Interface;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;
using Utilities.BaseClasses;
using Utilities.Managers;
using Utilities.ObjectPool;

namespace Actors
{
    public abstract class SpawnerMono : EnemyBase, IMoveToPointSubscriber
    {
        [SerializeField] protected Transform spawnPoint;
        protected ObjectPool pool;
        protected PositionPointsManager positionsManager;

        protected virtual void Awake()
        {
            positionsManager = GameManager.PositionManager;
            EventBroker.TriggerOnSpawnerRegistering();
            EventBroker.OnGameSessionStarted += OnGameSessionStarted;
            pool = GameManager.Pool;
        }
        
        protected abstract void OnGameSessionStarted();

        protected override void OnDisable()
        {
            base.OnDisable();
            EventBroker.OnGameSessionStarted -= OnGameSessionStarted;
        }

        public abstract void InitializeMoving(PositionPointsManager positionManager);

        public abstract void FreePositionAvailable(PositionPoint targetPosition);
    }
}