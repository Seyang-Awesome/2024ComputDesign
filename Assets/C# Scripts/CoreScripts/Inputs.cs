using System;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : BaseManager<Inputs>
{
    public bool IsEnable { get; private set; }
    private const float DefaultFloat = 0f;

    public float MouseX => IsEnable ? Input.GetAxis("Mouse X") : DefaultFloat;
    public float MouseY => IsEnable ? Input.GetAxis("Mouse Y") : DefaultFloat;
    public float MoveX => IsEnable ? Input.GetAxis("Horizontal") : DefaultFloat;
    public float MoveY => IsEnable ? Input.GetAxis("Vertical") : DefaultFloat;
    public bool IsTryToInteract => IsEnable && Input.GetMouseButtonDown(0);

    public void SetInputEnable(bool isEnable)
    {
        IsEnable = isEnable;
    }
}

