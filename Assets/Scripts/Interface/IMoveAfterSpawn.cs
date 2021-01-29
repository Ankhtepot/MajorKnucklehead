using UnityEngine;

namespace Interface
{
    public interface IMoveAfterSpawn
    {
        void InitializeMoving(PositionPointsManager positionManager);
        void FreePositionAvailable(PositionPointsManager.PositionPoint targetPosition);
    }
}