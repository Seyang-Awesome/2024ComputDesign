using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirWall : MonoBehaviour
{
    [SerializeField] private bool initIsTrigger;
    private Collider airCollider;
    private void Start()
    {
        airCollider = GetComponent<Collider>();
        SetColliderType(initIsTrigger);
    }

    public void SetColliderType(bool isTrigger)
    {
        if (airCollider == null) return;
        airCollider.isTrigger = isTrigger;
    }
}
