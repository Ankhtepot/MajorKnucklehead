using UnityEngine;
using Utilities;

//Fireball Games * * * PetrZavodny.com

public class EnemyHealth : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private int baseHealthPoints = 5;
    [SerializeField] private int currentHealthPoints;
    [SerializeField] private bool isDead;

    [Header("Assignables")]
    [SerializeField] private EnemyDeathHandler deathHandler;
#pragma warning restore 649

    void Start()
    {
        initialize();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isDead || !other.gameObject.CompareTag(Strings.PlayerDamage)) return;
        
        currentHealthPoints -= other.gameObject.GetComponent<Projectile>().damageAmount;

        if (currentHealthPoints > 0) return;
        
        isDead = true;
        deathHandler.HandleDeath();
    }

    private void initialize()
    {
        currentHealthPoints = baseHealthPoints;
    }
}
