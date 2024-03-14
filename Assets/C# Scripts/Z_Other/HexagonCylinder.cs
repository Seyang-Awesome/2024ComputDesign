using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 可修改边的柱体
/// </summary>
[RequireComponent(typeof(MeshFilter))]
public class HexagonCylinder : MonoBehaviour
{
    [SerializeField]
    private int sides = 6; // 地面的默认边数

    [SerializeField]
    private float radius = 1f; // 地面的半径

    [SerializeField]
    private float height = 1f; // 柱体的高度

    private MeshFilter meshFilter;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        GenerateMesh();
    }

    /// <summary>
    /// 修改边数
    /// </summary>
    /// <param name="newSides"></param>
    public void SetSides(int newSides)
    {
        sides = newSides;
        GenerateMesh();
    }

    private void GenerateMesh()
    {
        Mesh mesh = new Mesh();

        // 生成地面的顶点
        Vector3[] vertices = new Vector3[sides + 1];
        for (int i = 0; i < sides; i++)
        {
            float angle = 2f * Mathf.PI * i / sides;
            float x = radius * Mathf.Cos(angle);
            float z = radius * Mathf.Sin(angle);
            vertices[i] = new Vector3(x, 0f, z);
        }
        vertices[sides] = Vector3.zero; // 地面中心点

        // 生成柱体的顶点
        Vector3[] cylinderVertices = new Vector3[sides * 2 + 2];
        for (int i = 0; i < sides; i++)
        {
            Vector3 vertex = vertices[i];
            cylinderVertices[i] = vertex;
            cylinderVertices[i + sides] = new Vector3(vertex.x, height, vertex.z);
        }

        cylinderVertices[sides * 2] = Vector3.zero;
        cylinderVertices[sides * 2 + 1] = Vector3.up * height;

        // 生成三角形索引
        int[] triangles = new int[sides * 12];
        int triIndex = 0;
        for (int i = 0; i < sides; i++)
        {
            int nextIndex = (i + 1) % sides;
            // 地面三角形
            triangles[triIndex] = i;
            triangles[triIndex + 1] = nextIndex;
            triangles[triIndex + 2] = i + sides;
            // 柱体侧面三角形
            triangles[triIndex + 3] = nextIndex;
            triangles[triIndex + 4] = i + sides;
            triangles[triIndex + 5] = nextIndex + sides;

            triangles[triIndex + 6] = i;
            triangles[triIndex + 7] = nextIndex;
            triangles[triIndex + 8] = sides * 2;

            triangles[triIndex + 9] = i + sides;
            triangles[triIndex + 10] = nextIndex + sides;
            triangles[triIndex + 11] = sides * 2 + 1;
            triIndex += 12;
        }

        mesh.vertices = cylinderVertices;
        mesh.triangles = triangles;

        meshFilter.mesh = mesh;
    }
}