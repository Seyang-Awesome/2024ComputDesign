using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    public int numberOfSides; // 输入多边形的边数

    void Start()
    {
        Debug.Log("6:" + CalculatePolygonPerimeter(6, 1));
        Debug.Log("12:" + CalculatePolygonPerimeter(12, 1));
        Debug.Log("24:" + CalculatePolygonPerimeter(24, 1));
        Debug.Log("48:" + CalculatePolygonPerimeter(48, 1));
        Debug.Log("96:" + CalculatePolygonPerimeter(96, 1));
        Debug.Log("192:" + CalculatePolygonPerimeter(192, 1));
        Debug.Log("384:" + CalculatePolygonPerimeter(384, 1));
    }

    float CalculatePolygonPerimeter(int sides, float radius)
    {
        float angle = 2 * Mathf.PI / sides; // 计算内角
        float sideLength = 2 * radius * Mathf.Sin(angle / 2); // 计算边长
        return sides * sideLength / 2; // 返回周长
    }
}
