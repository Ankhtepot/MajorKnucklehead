using System;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities
{
    public class UnityCustomEvents
    {
        [Serializable] public class UnityVector3Event : UnityEvent<Vector3> {}
        // [Serializable] public class UnityGameStateAction : UnityAc<GameState, GameState> {}
        
    }
}