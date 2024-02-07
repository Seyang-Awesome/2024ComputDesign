using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnvironmentAnim : MonoBehaviour
{
    [SerializeField]
    private float skyBoxRotateSpeed = 0.002f;

    private void Update()
    {
        RotateSky();
    }

    /// <summary>
    /// 天空盒旋转
    /// </summary> 

    public void RotateSky()
    {
        float num = Camera.main.GetComponent<Skybox>().material.GetFloat("_Rotation");
        Camera.main.GetComponent<Skybox>().material.SetFloat("_Rotation", num + skyBoxRotateSpeed);
    }
}
