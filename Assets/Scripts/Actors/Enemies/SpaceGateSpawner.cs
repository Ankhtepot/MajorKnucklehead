

//Fireball Games * * * PetrZavodny.com

using DTOs;
using ScriptableObjects;
using UnityEngine;
using Utilities.Managers;

namespace Actors.Enemies
{
    public class SpaceGateSpawner : SpawnerMono
    {
#pragma warning disable 649
        [SerializeField] protected float birthDelay = 1f;
        [SerializeField] private WaveConfiguration wave;
#pragma warning restore 649


        protected override void OnGameSessionStarted()
        {
            
        }

        public override void InitializeMoving(PositionPointsManager positionManager)
        {
        }

        public override void FreePositionAvailable(PositionPoint targetPosition)
        {
        }
    }
}
