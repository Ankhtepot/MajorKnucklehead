using System;
using Actors.Enemies;
using UnityEngine;

namespace DTOs
{
    [Serializable]
    public class EnemySequenceSlot
    {
        public GameObject enemyPrefab;
        public int spawnTimes;
    }
}