﻿using Enumerations;
using UnityEngine;
using Utilities.Extensions;
using Utilities.ObjectPool;

//Fireball Games * * * PetrZavodny.com

public class Gun : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] Projectile projectile;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private ObjectPool Pool;
    [Range(0.1f, 5f)] [SerializeField] private float clickPosMultiplier = 5f;
    [SerializeField] private Camera mainCamera;
#pragma warning restore 649

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (GameManager.CurrentGameState != GameState.Running) return;
            
            OnShoot();
        }
    }

    public void OnShoot()
    {
        var plane = new Plane(Vector3.back, transform.position);
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        
        if (!plane.Raycast(ray, out var hitDistance)) return;

        var clickPos = ray.GetPoint(hitDistance);
        
        var spawnPosition = projectileSpawnPoint.position;
        clickPos = clickPos.ZToZero() * clickPosMultiplier;
        
        var newProjectile = Pool.GetFromPool(projectile.gameObject, spawnPosition, Quaternion.LookRotation(Input.mousePosition));
        // newProjectile.Pool = Pool;
            
        var directionVector = (clickPos - spawnPosition).normalized;
        // print($"Cannon fires {projectile.name}. Direction vector: {directionVector}. Click position: {clickPos}, SpawnPoint position: {spawnPosition}");
        newProjectile.GetComponent<Projectile>().AddVelocity(directionVector.normalized);
    }
}
