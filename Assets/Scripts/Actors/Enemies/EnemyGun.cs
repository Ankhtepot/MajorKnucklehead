using System.Collections;
using UnityEngine;
using Utilities;
using Utilities.ObjectPool;

//Fireball Games * * * PetrZavodny.com

namespace Actors.Enemies
{
    public class EnemyGun : ExtendedMono, IPoolNeedy
    {
#pragma warning disable 649
        [SerializeField] private float shootCooldown = 0.5f;
        [SerializeField] private int projectileSpeed;
        [SerializeField] private float projectileLifetime = 60f;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform projectileSpawnPoint;
        public bool CanShoot { set => SetCanShoot(value); } 
        public ObjectPool pool { get; set; }
        
        private bool _canShoot;
#pragma warning restore 649

        private void SetCanShoot(bool canShoot)
        {
            _canShoot = canShoot;
            if (_canShoot)
            {
                StartCoroutine(ShootRoutine());
                return;
            }
            
            StopAllCoroutines();
        }

        private IEnumerator ShootRoutine()
        {
            while (_canShoot)
            {
                yield return new WaitForSeconds(shootCooldown);

                var newProjectile = pool.GetFromPool(projectilePrefab, projectileSpawnPoint.position, Quaternion.LookRotation(Vector3.down)).GetComponent<Projectile>();
                newProjectile.SetAndLaunch(Vector3.down, projectileSpeed, projectileLifetime);
            }
        }

        private void OnDisable()
        {
            _canShoot = false;
        }
    }
}
