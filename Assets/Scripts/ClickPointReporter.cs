using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

//Fireball Games * * * PetrZavodny.com

public class ClickPointReporter : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private GameObject plane;
    [SerializeField] public UnityCustomEvents.UnityVector3Event OnClick;
    public LayerMask NonBLockingLayer;
    private Camera mainCamera;
#pragma warning restore 649

    void Start()
    {
        initialize();
    }

    private void OnMouseDown()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            OnClick?.Invoke(hit.point);
        }
    }

    private void initialize()
    {
        mainCamera = FindObjectOfType<Camera>();
    }
}
