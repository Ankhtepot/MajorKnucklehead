using UnityEngine;

namespace Interface
{
    public interface IMoveAfterSpawn
    {
        void InitializeMoving(PositionPointsManager positionManager);
        void StartMoving(Vector3 targetPosition);
    }
}