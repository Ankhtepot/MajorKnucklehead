using UnityEngine;
using Utilities;

//Fireball Games * * * PetrZavodny.com

namespace Actors.Player
{
    public class PlayerMovement : MonoBehaviour
    {
#pragma warning disable 649
        [SerializeField] private float moveSpeed = 10;
        [SerializeField] private float moveDrag = 1f;
        [SerializeField] private float stopDrag = 3f;
        [Header("Assignables")] 
        [SerializeField] private Rigidbody rigidBody;
#pragma warning restore 649

        void Update()
        {
            ManageInput();
        }

        private void ManageInput()
        {
            // if (GameManager.CurrentGameState != GameState.Running) return;
        
            var xAxis = Input.GetAxis(Strings.Horizontal);
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
    }
}
