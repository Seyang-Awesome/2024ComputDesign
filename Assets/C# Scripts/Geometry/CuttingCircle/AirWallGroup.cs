using System;
using System.Collections.Generic;
using UnityEngine;

public class AirWallGroup : MonoBehaviour
{
    [SerializeField] private List<AirWall> airWalls = new();
    // [SerializeField] private StepTrigger stepTrigger;

    // private void Start()
    // {
    //     stepTrigger.OnTrigger.AddListener(OnPlayerEnter);
    // }
    //
    // private void OnDestroy()
    // {
    //     stepTrigger.OnTrigger.RemoveListener(OnPlayerEnter);
    // }

    public void OnPlayerEnter()
    {
        airWalls.ForEach(airWall =>
        {
            airWall.SetColliderType(false);
        });
    }

    public void OnPlayerFinished()
    {
        airWalls.ForEach(airWall =>
        {
            airWall.SetColliderType(true);
        });
    }
}

