using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class Projectile : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private float projectileSpeed = 20;

    [HideInInspector] public ObjectPool Pool;
#pragma warning restore 649

    void Start()
    {
        initialize();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        Pool.ReturnToPool(this);
    }

    public void AddVelocity(Vector3 force)
    {
        rigidBody.velocity = force * projectileSpeed * Time.deltaTime;
    }
    
    private void initialize()
    {
       
    }
}
