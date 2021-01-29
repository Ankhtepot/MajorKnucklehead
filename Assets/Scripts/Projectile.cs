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

    private IEnumerator StartLifeCooldown()
    {
        yield return new WaitForSeconds(lifetime);
        ReturnToPool();
    }

    private void OnCollisionEnter(Collision other)
    {
        // print($"Projectile collided with: {other.gameObject.name}");
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        pool.ReturnToPool(gameObject);
    }

    public void SetAndLaunch(Vector3 force, int projectileSpeedOverwrite = -1, float lifetimeOverwrite = -1)
    {
        lifetime = lifetimeOverwrite > 0 ? lifetimeOverwrite : lifetime; 
        
        rigidBody.velocity = force * (
            projectileSpeedOverwrite <= 0 ? projectileSpeed : projectileSpeedOverwrite
            * Time.deltaTime);
        
        StartCoroutine(StartLifeCooldown());
    }
}
