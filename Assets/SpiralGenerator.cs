using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpiralGenerator : MonoBehaviour
{
    public GameObject prefab; // 物体预制体
    public int objectCount; // 生成的物体数量
    public float radius = 5f; // 环的半径
    public float direction;
    public float tiltAngle = 30f; // 倾斜度
    public float distanceBetweenObjects = 1f; // 物体之间的距离
    public float rotationSpeed = 30f; // 旋转速度
    public Vector3 rotationDirection = Vector3.forward;
    private List<GameObject> objects = new List<GameObject>();
    private float currentRotation = 0f;

    void OnEnable()
    {
        EventCenter.GetInstance().AddEventListener<float>("ChangeSpiralRotateSpeed",ChangeRotationSpeed);
    }

    private void OnDisable()
    {
        EventCenter.GetInstance().RemoveEventListener<float>("ChangeSpiralRotateSpeed",ChangeRotationSpeed);
    }


    void Start()
    {
        GenerateSpiral();
        transform.Rotate(Vector3.up*direction);
    }

    void ChangeRotationSpeed(float speed) => rotationSpeed = speed;
    
    void Update()
    {
        RotateSpiral();
    }

    void GenerateSpiral()
    {
        for (int i = 0; i < objectCount; i++)
        {
            float angle = i * Mathf.PI * 2f / objectCount;
            Vector3 position = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius * Mathf.Cos(tiltAngle * Mathf.Deg2Rad), Mathf.Sin(angle) * radius * Mathf.Sin(tiltAngle * Mathf.Deg2Rad));
            GameObject obj = Instantiate(prefab, transform.position + position, Quaternion.identity,transform);
            objects.Add(obj);
        }
    }

    void RotateSpiral()
    {
        transform.RotateAround(transform.position, rotationDirection , rotationSpeed * Time.deltaTime);
    }

    public void SetRotationSpeed(float speed)
    {
        rotationSpeed = speed;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, rotationDirection*10 + transform.position);
    }
}

