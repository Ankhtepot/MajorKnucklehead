using DTOs;
using Interface;
using UnityEngine;
using Utilities.Managers;

namespace Actors
{
    public abstract class MovingSpawnerMono : SpawnerMono, IMoveToPointSubscriber
    {
        [SerializeField] protected MoverToPosition mover;
        public abstract void InitializeMoving(PositionPointsManager positionManager);

        public abstract void FreePositionAvailable(PositionPoint targetPosition);
        
    }
}