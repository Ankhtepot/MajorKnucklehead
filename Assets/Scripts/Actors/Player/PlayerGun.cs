using System.Collections;
using Enumerations;
using UnityEngine;
using Utilities.Extensions;
using Utilities.Managers;
using Utilities.ObjectPool;
using Zenject;

//Fireball Games * * * PetrZavodny.com

namespace Actors.Player
{
    public class PlayerGun : MonoBehaviour, IPoolInitializable, IPoolNeedy
    {
#pragma warning disable 649
        [SerializeField] private float canShootCooldown;
        [SerializeField] Projectile projectile;
        [SerializeField] private Transform projectileSpawnPoint;
        [Range(0.1f, 5f)] [SerializeField] private float clickPosMultiplier = 5f;
        [SerializeField] private Camera mainCamera;
        
        public ObjectPool pool { get; set; }

        private bool canShoot = true;
        private GameManager _gameManager;
#pragma warning restore 649

        private void Update()
        {
            if (canShoot && Input.GetMouseButton(0) && GameManager.CurrentGameState == GameState.Running)
            {
                OnShoot();
            }
        }

        private void OnShoot()
        {
            canShoot = false;
            StartCoroutine(CanShootCooldownRoutine());
            
            var plane = new Plane(Vector3.back, Vector3.zero);
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        
            if (!plane.Raycast(ray, out var hitDistance)) return;

            var clickPos = ray.GetPoint(hitDistance);
            var spawnPosition = projectileSpawnPoint.position;
            
            var distanceToClickPos = Vector3.SqrMagnitude(clickPos - spawnPosition);
            // print($"distanceToClickPos: {distanceToClickPos}");

            if (distanceToClickPos < 7f) return;
        
            clickPos = clickPos.ZToZero();;
            
            var directionVector = (clickPos - spawnPosition).normalized;
            // print($"Cannon fires {projectile.name}. Direction vector: {directionVector}. Click position: {clickPos}, SpawnPoint position: {spawnPosition}");
            var newProjectile = pool.GetFromPool(projectile.gameObject, spawnPosition, Quaternion.LookRotation(Input.mousePosition));
            newProjectile.GetComponent<Projectile>().SetAndLaunch(directionVector);
        }

        private IEnumerator CanShootCooldownRoutine()
        {
            yield return new WaitForSeconds(canShootCooldown);
            canShoot = true;
        }

        public void Initialize()
        {
            mainCamera = GameManager.MainCamera;
            projectile = GameManager.AmmoManager.CurrentProjectile;
            canShoot = true;
        }
    }
}
