using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

[CreateAssetMenu(menuName = "Sciptable Objects/Mother Ship Spawn Configuration")]
public class MotherShipSpawnConfiguration : ScriptableObject
{
#pragma warning disable 649
    public int enemiesCount = 1;
    public float spawnFrequency = 0.1f;
    public List<Enemy> possibleEnemies = new List<Enemy>();
#pragma warning restore 649
}
