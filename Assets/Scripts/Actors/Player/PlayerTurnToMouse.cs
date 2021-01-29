using UnityEngine;

//Fireball Games * * * PetrZavodny.com

namespace Actors.Player
{
    public class PlayerTurnToMouse : MonoBehaviour
    {
#pragma warning disable 649
        [SerializeField] private float bodyRotationSpeed = 20;
        [Header("Assignables")]
        [SerializeField] private Transform playerBody;

        private Camera mainCamera;
#pragma warning restore 649

        void Start()
        {
            initialize();
        }

        void Update()
        {
            RotateBodyToMouse();
        }

        private void RotateBodyToMouse()
        {
            // Generate a plane that intersects the transform's position with an upwards normal.
            var playerPlane = new Plane(Vector3.up, playerBody.position);
        
            // Generate a ray from the cursor position
            var ray = mainCamera.ScreenPointToRay (Input.mousePosition);
        
            // Determine the point where the cursor ray intersects the plane.
            // This will be the point that the object must look towards to be looking at the mouse.
            // Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
            //   then find the point along that ray that meets that distance.  This will be the point
            //   to look at.

            // If the ray is parallel to the plane, Raycast will return false.
            if (!playerPlane.Raycast(ray, out var hitDistance)) return;
        
            // Get the point along the ray that hits the calculated distance.
            var targetPoint = ray.GetPoint(hitDistance);
        
            // Determine the target rotation.  This is the rotation if the transform looks at the target point.
            var targetRotation = Quaternion.LookRotation(targetPoint - playerBody.position);
        
            // Smoothly rotate towards the target point.
            playerBody.rotation = Quaternion.Slerp(playerBody.rotation, targetRotation, bodyRotationSpeed * Time.deltaTime);
        }

        private void initialize()
        {
            mainCamera = FindObjectOfType<Camera>();
        }
    }
}
