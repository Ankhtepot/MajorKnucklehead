using System.Collections;
using UnityEngine;
using Utilities.ObjectPool;

//Fireball Games * * * PetrZavodny.com

public class Projectile : MonoBehaviour, IPoolNeedy
{
#pragma warning disable 649
    public int damageAmount = 1;
    [SerializeField] private float projectileSpeed = 1000;
    [SerializeField] private float lifetime = 0.5f;
    [Header("Assignables")]
    [SerializeField] private Rigidbody rigidBody;

    public ObjectPool pool { get; set; }
#pragma warning restore 649

    void Start()
    {
        initialize();
    }

    private IEnumerator StartLifeCooldown()
    {
        yield return new WaitForSeconds(lifetime);
        ReturnToPool();
    }

    private void OnCollisionEnter(Collision other)
    {
        print($"Projectile collided with: {other.gameObject.name}");
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        pool.ReturnToPool(this);
    }

    public void AddVelocity(Vector3 force)
    {
        StartCoroutine(StartLifeCooldown());
        rigidBody.velocity = force * (projectileSpeed * Time.deltaTime);
    }
    
    private void initialize()
    {
       
    }
}
