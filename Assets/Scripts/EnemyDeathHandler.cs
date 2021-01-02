using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class EnemyDeathHandler : MonoBehaviour
{
#pragma warning disable 649
    private ObjectPool Pool;
#pragma warning restore 649

    void Start()
    {
        initialize();
    }

    void Update()
    {
        
    }

    public void HandleDeath()
    {
        ReturnToPool();
    }
    
    private void ReturnToPool()
    {
        Pool.ReturnToPool(this);
    }
    
    private void initialize()
    {
        Pool = FindObjectOfType<ObjectPool>();
    }
}
