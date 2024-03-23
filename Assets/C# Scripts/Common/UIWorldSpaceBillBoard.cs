using System;
using System.Collections.Generic;
using UnityEngine;

public class UIWorldSpaceBillBoard : MonoBehaviour
{
    private Transform mainCameraTransform;

    private void Start()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
            canvas.worldCamera = Camera.main;
        mainCameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        transform.LookAt(mainCameraTransform);
        transform.Rotate(Vector3.up, 180, Space.Self);
    }
}

