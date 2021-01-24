using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interface;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class PositionPointsManager : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private float checkForFreePositionPeriod = 1f;
    [SerializeField] private List<PositionPoint> positions = new List<PositionPoint>();
    private Queue<IMoveAfterSpawn> waitQueue = new Queue<IMoveAfterSpawn>();
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
        yield return new WaitForSeconds(checkForFreePositionPeriod);

        if (waitQueue.Any() && positions.Any(position => !position.occupied))
        {
            var freePosition = positions.Find(position => !position.occupied);
            freePosition.occupied = true;
            waitQueue.Dequeue().StartMoving(freePosition.Position);
        }
    }

    public void RequestFreePositionWhenAvailable(IMoveAfterSpawn subscriber)
    {
        waitQueue.Enqueue(subscriber);
    }    
    
    private void initialize()
    {
        for(int i = 0; i < transform.childCount; i++)        
        {
            positions.Add(new PositionPoint() { transform = transform.GetChild(i).transform, occupied = false});
        }
    }
    
    [Serializable]
    private class PositionPoint
    {
        public Transform transform;
        public bool occupied;

        public Vector3 Position => transform.position;
    }
}
