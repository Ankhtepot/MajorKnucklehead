using UnityEngine;
using Utilities.ObjectPool;

namespace Utilities.BaseClasses
{
    public abstract class HealthBase : MonoBehaviour, IPoolInitializable
    {
#pragma warning disable 649
        [SerializeField] protected int baseHealthPoints = 5;
        [SerializeField] protected int currentHealthPoints;
        [SerializeField] protected bool isDead;

        [Header("Assignables")]
        [SerializeField] protected DeathHandlerBase deathHandler;

        protected string DamagingTag { get; set; }
#pragma warning restore 649

        protected abstract void SetDamagingTag();
        
        private void Awake()
        {
            initialize();
            SetDamagingTag();
        }
        
        protected virtual void OnCollisionEnter(Collision other)
        {
            if (isDead || !other.gameObject.CompareTag(DamagingTag)) return;
        
            currentHealthPoints -= other.gameObject.GetComponent<Projectile>().damageAmount;

            if (currentHealthPoints > 0) return;
        
            isDead = true;
            deathHandler.HandleDeath();
        }

        private void initialize()
        {
            currentHealthPoints = baseHealthPoints;
        }

        public void Initialize()
        {
            isDead = false;
            initialize();
        }
    }
}