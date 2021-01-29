using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

//Fireball Games * * * PetrZavodny.com

public class MoverToPosition : ExtendedMono
{
#pragma warning disable 649
    [SerializeField] private float moveSpeed = 10f;
    public UnityEvent OnPositionReached;

    [SerializeField] private PositionPointsManager.PositionPoint positionPoint;
#pragma warning restore 649

    public void StartMovingToPosition(PositionPointsManager.PositionPoint targetPosition)
    {
        StartCoroutine(MovingRoutine(targetPosition));
    }
    
    private IEnumerator MovingRoutine(PositionPointsManager.PositionPoint targetPosition)
    {
        positionPoint = targetPosition;
        Debug.Log("Started moving to position.");
        
        while (Vector3.SqrMagnitude(targetPosition.Position - transform.position) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.Position, moveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        transform.parent = positionPoint.transform;
        
        OnPositionReached?.Invoke();
    }

    private void OnDisable()
    {
        OnPositionReached.RemoveAllListeners();
        transform.localPosition = new Vector3(0,0,0);
        
        if (positionPoint != null)
        {
            positionPoint.occupied = false;
            positionPoint = null;
        }
    }
}
