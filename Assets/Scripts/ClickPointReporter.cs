using System;
using UnityEngine;
using Utilities;

//Fireball Games * * * PetrZavodny.com

public class ClickPointReporter : MonoBehaviour
{
#pragma warning disable 649
    public LayerMask NonBLockingLayer;
    [SerializeField] public UnityCustomEvents.UnityVector3Event OnClick;
    [SerializeField] private Transform playerPositionPivot;
    private Camera mainCamera;
#pragma warning restore 649

    void Start()
    {
        initialize();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseDown();
        }
    }

    private void OnMouseDown()
    {
        // var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        // RaycastHit hit;
        //
        // if (Physics.Raycast(ray, out hit, 1000f, ~NonBLockingLayer))
        // {
        //     OnClick?.Invoke(hit.point);
        // }
        
        // var plane = new Plane(Vector3.back, playerPositionPivot.position);
        // var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        //
        // if (!plane.Raycast(ray, out var hitDistance)) return;
        //
        // OnClick?.Invoke(ray.GetPoint(hitDistance));
    }

    private void initialize()
    {
        mainCamera = FindObjectOfType<Camera>();
    }
}
