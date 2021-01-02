using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//Fireball Games * * * PetrZavodny.com

public class Gun : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] Projectile projectile;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private ObjectPool Pool;
    
    private Camera mainCamera;
    private Vector3 clickPosition = Vector3.zero;
#pragma warning restore 649

    void Start()
    {
        initialize();
    }

    void Update()
    {
        ManageInput();
    }

    private void ManageInput()
    {
        // if (Input.GetButtonDown("Fire1"))
        // {
        //     var newProjectile = Pool.GetFromPool(projectile, projectileSpawnPoint.position,
        //         Quaternion.LookRotation(Input.mousePosition));
        //     
        //     var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        //     mousePosition.z = 0;
        //     clickPosition = mousePosition;
        //
        //     var directionVector = (mousePosition - projectileSpawnPoint.position).normalized; 
        //     print($"Cannon fires {projectile.name}. Direction vector: {directionVector}. Mouse position: {mousePosition}, Spawnpoint position: {projectileSpawnPoint.position}");
        //     // newProjectile.damage = damage;
        //     newProjectile.AddVelocity(directionVector.normalized);
        // }
    }

    public void OnPlaneClicked(Vector3 clickPos)
    {
        var newProjectile = Pool.GetFromPool(projectile, projectileSpawnPoint.position,
            Quaternion.LookRotation(Input.mousePosition));
        newProjectile.Pool = Pool;
            
        clickPosition = clickPos;
 
        var directionVector = (clickPos - projectileSpawnPoint.position).normalized;
        // directionVector.z = 0;
        // print($"Cannon fires {projectile.name}. Direction vector: {directionVector}. Click position: {clickPos}, Spawnpoint position: {projectileSpawnPoint.position}");
        // newProjectile.damage = damage;
        newProjectile.AddVelocity(directionVector.normalized);
    }

    // private void OnMouseDown()
    // {
    //     var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
    //     var plane = new Plane(Vector3.back, Vector3.zero);
    //     float distance;
    //     RaycastHit hit;
    //     if (plane.Raycast(ray, out distance))
    //     {
    //         Debug.Log("Plane Raycast hit at distance: " + distance);
    //         var hitPoint = ray.GetPoint(distance);
    //        
    //         var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
    //         go.transform.position = hitPoint;
    //         Debug.DrawRay (ray.origin, ray.direction * distance, Color.green);
    //     }
    // }

    // private void OnDrawGizmos()
    // {
    //     if (clickPosition != Vector3.zero)
    //     {
    //         Gizmos.DrawLine(projectileSpawnPoint.position, clickPosition);
    //     }
    // }

    private void initialize()
    {
        mainCamera = FindObjectOfType<Camera>();
    }
}
