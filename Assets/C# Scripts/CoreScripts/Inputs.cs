using System;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : BaseManager<Inputs>
{
    public bool IsEnable { get; private set; }  //是否禁用输入
    private const float DefaultFloat = 0f;      //禁用输入时返回的默认值

    public float MouseX => IsEnable ? Input.GetAxis("Mouse X") : DefaultFloat;
    public float MouseY => IsEnable ? Input.GetAxis("Mouse Y") : DefaultFloat;
    public float MoveX => IsEnable ? Input.GetAxis("Horizontal") : DefaultFloat;
    public float MoveY => IsEnable ? Input.GetAxis("Vertical") : DefaultFloat;
    public bool IsTryToInteract => IsEnable && Input.GetMouseButtonDown(0);

    /// <summary>
    /// 设置输入状态
    /// </summary>
    public void SetInputEnable(bool isEnable)
    {
        IsEnable = isEnable;
    }
}

