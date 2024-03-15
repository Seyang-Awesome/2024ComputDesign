using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirWall : MonoBehaviour
{
    [SerializeField] private bool initIsTrigger;
    private Collider collider;
    private void Start()
    {
        collider = GetComponent<Collider>();
        SetColliderType(initIsTrigger);
    }

    public void SetColliderType(bool isTrigger)
    {
        if (collider == null) return;
        collider.isTrigger = isTrigger;
    }
}
