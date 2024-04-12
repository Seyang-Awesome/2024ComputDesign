using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbacusDecoration : MonoBehaviour
{
    public Transform rotationCenter;
    public Vector3 rotationAxis = Vector3.up;
    public float rotationSpeed = 10f;
    public bool end;

    private Vector3 lastDir;

    private void Update()
    {
        if (!end)
        {
            // 绕轴心旋转
            Vector3 lastPos = transform.position;
            transform.RotateAround(rotationCenter.position, rotationAxis, rotationSpeed * Time.deltaTime);
            lastDir = transform.position - lastPos;
        }
        else
        {
            transform.position += lastDir * 50 * Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // 显示轴心
        if (rotationCenter != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(rotationCenter.position, 0.1f);
        }

        // 显示旋转轴
        Gizmos.color = Color.red;
        Gizmos.DrawLine(rotationCenter.position + rotationAxis * 10, rotationCenter.position - rotationAxis * 10);
        Gizmos.DrawLine(rotationCenter.position,transform.position);
    }
}
