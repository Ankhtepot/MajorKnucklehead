using UnityEngine;

//Fireball Games * * * PetrZavodny.com

namespace Utilities.Managers
{
    public class CameraManager : MonoBehaviour
    {
#pragma warning disable 649
        [SerializeField] private Camera mainCamera;

        public Camera MainCamera => mainCamera;
#pragma warning restore 649


    }
}
