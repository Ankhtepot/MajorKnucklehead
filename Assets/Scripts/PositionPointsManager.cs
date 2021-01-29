using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enumerations;
using Interface;
using UnityEngine;
using Utilities;

//Fireball Games * * * PetrZavodny.com

public class PositionPointsManager : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private float checkForFreePositionPeriod = 1f;
    [SerializeField] private List<PositionPoint> positions = new List<PositionPoint>();
    [SerializeField] private Queue<IMoveAfterSpawn> waitQueue = new Queue<IMoveAfterSpawn>();
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

            if (waitQueue.Any() && positions.Any(position => !position.occupied))
            {
                var freePosition = positions.Find(position => !position.occupied);
                freePosition.occupied = true;
                waitQueue.Dequeue().FreePositionAvailable(freePosition);
            }
        }
    }

    public void RequestFreePositionWhenAvailable(IMoveAfterSpawn subscriber)
    {
        waitQueue.Enqueue(subscriber);
    }    
    
    private void initialize()
    {
        EventBroker.OnGameStateChanged += OnGameStateChanged;
        
        for(int i = 0; i < transform.childCount; i++)        
        {
            positions.Add(new PositionPoint() { transform = transform.GetChild(i).transform, occupied = false});
        }
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


    [Serializable]
    public class PositionPoint
    {
        public Transform transform;
        public bool occupied;

        public Vector3 Position => transform.position;
    }
}
