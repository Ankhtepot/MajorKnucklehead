using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DTOs;
using Enumerations;
using Interface;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

namespace Utilities.Managers
{
    public class PositionPointsManager : MonoBehaviour
    {
#pragma warning disable 649
        [SerializeField] private float checkForFreePositionPeriod = 1f;
        [SerializeField] private Dictionary<PositionPointType, List<PositionPoint>> positions;
        [SerializeField] private Dictionary<PositionPointType, Queue<IMoveToPointSubscriber>> waitQueue;
        private GameState currentGameState;
#pragma warning restore 649

        private void Awake()
        {
            initialize();
        }

        private void Start()
        {
            StartCoroutine(ServeFreePositionRoutine());
        }

        private IEnumerator ServeFreePositionRoutine()
        {
            while (currentGameState == GameState.Running)
            {
                yield return new WaitForSeconds(checkForFreePositionPeriod);

               DequeByPositionType(PositionPointType.Ship);
               DequeByPositionType(PositionPointType.MotherShipAtPlayer);
               DequeByPositionType(PositionPointType.MotherShipFromPortal);
            }
        }

        private void DequeByPositionType(PositionPointType positionType)
        {
            if (waitQueue[positionType].Any() && positions[positionType].Any(position => !position.occupied))
            {
                // var freePosition = positions.Find(position => position.positionType == positionType && !position.occupied);
                var freePositions = positions[positionType].Where(position => !position.occupied).ToArray();
                var freePosition = freePositions.ElementAt(Random.Range(0, freePositions.Count()));
                freePosition.occupied = true;
                waitQueue[positionType].Dequeue().FreePositionAvailable(freePosition);
            }
        }

        public void RequestFreePositionWhenAvailable(IMoveToPointSubscriber subscriber, PositionPointType positionType)
        {
            waitQueue[positionType].Enqueue(subscriber);
        }    
        
        private void OnGameStateChanged(GameState previousState, GameState currentState)
        {
            currentGameState = currentState;
        
            StopCoroutine(ServeFreePositionRoutine());
        
            if (currentGameState == GameState.Running)
            {
                StartCoroutine(ServeFreePositionRoutine());
            }
        }

        private void OnDisable()
        {
            EventBroker.OnGameStateChanged -= OnGameStateChanged;
        }

        private void initialize()
        {
            waitQueue = new Dictionary<PositionPointType, Queue<IMoveToPointSubscriber>>()
            {
                {PositionPointType.Ship, new Queue<IMoveToPointSubscriber>()},
                {PositionPointType.MotherShipAtPlayer, new Queue<IMoveToPointSubscriber>()},
                {PositionPointType.MotherShipFromPortal, new Queue<IMoveToPointSubscriber>()},
            };

            positions = new Dictionary<PositionPointType, List<PositionPoint>>();
            var rawPositions = GetComponentsInChildren<PositionPoint>();
            positions.Add(PositionPointType.Ship, rawPositions.Where(position => position.positionType == PositionPointType.Ship).ToList());
            positions.Add(PositionPointType.MotherShipAtPlayer, rawPositions.Where(position => position.positionType == PositionPointType.MotherShipAtPlayer).ToList());
            positions.Add(PositionPointType.MotherShipFromPortal, rawPositions.Where(position => position.positionType == PositionPointType.MotherShipFromPortal).ToList());
            
            EventBroker.OnGameStateChanged += OnGameStateChanged;
        }
    }
}
