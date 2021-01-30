using DTOs;
using Enumerations;
using UnityEngine;
using Utilities.Managers;

namespace Interface
{
    public interface IMoveToPointSubscriber
    {
        void InitializeMoving(PositionPointsManager positionManager);
        void FreePositionAvailable(PositionPoint targetPosition);
    }
}