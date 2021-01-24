using Enumerations;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class LookAtMouse : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private Transform Cannon;
    [Range(0.1f, 10f)] [SerializeField] private float midPointMultiplier = 0.5f;

    private Camera mainCamera;
#pragma warning restore 649

    void Start()
    {
        initialize();
    }

    void Update()
    {
        if (GameManager.CurrentGameState != GameState.Running) return;
        
        var mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        float midPoint = (Cannon.position - mainCamera.transform.position).magnitude * midPointMultiplier;
        
        Cannon.LookAt(mouseRay.origin + mouseRay.direction * midPoint);
    
        // For orthographic camera
        // Cannon.LookAt(mainCamera.ScreenToWorldPoint(Input.mousePosition)); 
    }
    
    private void initialize()
    {
       mainCamera = Camera.main;
    }
}
