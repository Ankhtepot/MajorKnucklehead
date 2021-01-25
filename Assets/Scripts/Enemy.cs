using System.Collections;
using System.Collections.Generic;
using Interface;
using UnityEngine;
using Utilities;

//Fireball Games * * * PetrZavodny.com

[RequireComponent(typeof(MoverToPosition))]
public class Enemy : ExtendedMono, IMoveAfterSpawn
{
#pragma warning disable 649
    [HideInInspector] public bool onShootPosition;
    private bool canShoot;
#pragma warning restore 649

    public void InitializeMoving(PositionPointsManager positionManager)
    {
        positionManager.RequestFreePositionWhenAvailable(this);
    }

    public void StartMoving(Vector3 targetPosition)
    {
        GetComponent<MoverToPosition>().StartMovingToPosition(targetPosition);
    }
}
