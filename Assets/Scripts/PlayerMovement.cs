using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class PlayerMovement : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float moveDrag = 1f;
    [SerializeField] private float stopDrag = 3f;
    [Header("Assignables")] 
    [SerializeField] private Rigidbody rigidBody;
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
        var xAxis = Input.GetAxis("Horizontal");
        if (xAxis != 0)
        {
            rigidBody.drag = moveDrag;
            rigidBody.AddForce(xAxis * Time.deltaTime * moveSpeed, 0, 0);
        }
        else
        {
            rigidBody.drag = stopDrag;
        }
    }

    private void initialize()
    {
       
    }
}
