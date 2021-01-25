using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

//Fireball Games * * * PetrZavodny.com

public class MoverToPosition : ExtendedMono
{
#pragma warning disable 649
    [SerializeField] private float moveSpeed = 10f;
#pragma warning restore 649

    public void StartMovingToPosition(Vector3 targetPosition)
    {
        StartCoroutine(MovingRoutine(targetPosition));
    }
    
    private IEnumerator MovingRoutine(Vector3 targetPosition)
    {
        while (Vector3.SqrMagnitude(targetPosition - transform.position) > 0.1f)
        {
            Debug.Log("Started moving to position.");
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnDisable()
    {
        //TODO: free occupied position
    }
}
