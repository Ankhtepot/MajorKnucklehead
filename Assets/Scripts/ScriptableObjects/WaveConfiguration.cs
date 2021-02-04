using System.Collections.Generic;
using DTOs;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Enemy Wave Configuration", menuName = "Configurations/Wave Configuration", order = 0)]
    public class WaveConfiguration : ScriptableObject
    {
        public List<EnemySequenceSlot> EnemiesToSpawn;
        public bool spawnInSequence = true;
        public bool spawnInRegularIntervals = true;
        public float initialDelay = 1f;
        public float timeBetweenSpawns = 1f;
    }
}