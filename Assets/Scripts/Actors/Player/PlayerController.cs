using Unity.Mathematics;
using UnityEngine;
using Utilities;
using Utilities.Managers;
using Utilities.ObjectPool;

//Fireball Games * * * PetrZavodny.com

namespace Actors.Player
{
    public class PlayerController : MonoBehaviour
    {
#pragma warning disable 649
        [SerializeField] private GameObject playerPrefab;

        private ObjectPool _pool;
        private GameObject _spawnedPlayer;
#pragma warning restore 649

        private void Awake()
        {
            EventBroker.OnGameSessionStarted += OnGameSessionStarted;
            EventBroker.OnGameSessionStopped += OnGameSessionStopped;
            
            _pool = GameManager.Pool;
        }

        private void OnGameSessionStarted()
        {
            _spawnedPlayer = _pool.GetFromPool(playerPrefab, Vector3.zero, quaternion.identity);
        }

        private void OnGameSessionStopped()
        {
            _pool.ReturnToPool(_spawnedPlayer);
        }
    }
}
