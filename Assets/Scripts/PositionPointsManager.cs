using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class PositionPointsManager : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private List<PositionPoint> positions = new List<PositionPoint>();
#pragma warning restore 649

    void Start()
    {
        initialize();
    }

    void Update()
    {
        
    }
    
    private void initialize()
    {
        for(int i = 0; i < transform.childCount; i++)        
        {
            positions.Add(new PositionPoint() { position = transform.GetChild(i).transform, occupied = false});
        }
    }
    
    [Serializable]
    private class PositionPoint
    {
        public Transform position;
        public bool occupied;
    }
}
