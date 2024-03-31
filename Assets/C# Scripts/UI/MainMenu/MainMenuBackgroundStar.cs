using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class MainMenuBackgroundStar : MonoBehaviour
{
    [SerializeField]
    private GameObject starPrefab;
    public float height = 10f; // 生成星星的高度
    public float minInterval = 1f; // 最小生成间隔
    public float maxInterval = 3f; // 最大生成间隔
    public float minX = -5f; // x坐标范围最小值
    public float maxX = 5f; // x坐标范围最大值
    public float minSpeed = 1f; // 速度最小值
    public float maxSpeed = 3f; // 速度最大值

    private float timer = 0f;
    private float interval;

    private void Start()
    {
        interval = Random.Range(minInterval, maxInterval);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            GenerateStar();
            timer = 0f;
            interval = Random.Range(minInterval, maxInterval);
        }
    }

    private void GenerateStar()
    {
        // 在高度范围内随机选择x坐标
        float xPos = Random.Range(minX, maxX);
        // 在预设的高度上生成星星
        Vector3 spawnPosition = new Vector3(xPos, height, transform.position.z); // 在高度上生成星星，z轴随机
        GameObject newStar = Instantiate(starPrefab, spawnPosition, Quaternion.identity);
        // 为星星赋予一个斜向下的速度
        float speed = Random.Range(minSpeed, maxSpeed);
        Rigidbody rb = newStar.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = new Vector3(-1f, -1f, 0f).normalized; // 斜向下速度，可调整方向
            rb.velocity = direction * speed;
        }
    }

    private void OnDrawGizmos()
    {
        // 绘制高度范围内的x范围
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(minX, height, transform.position.z), new Vector3(maxX, height, transform.position.z));
        // 绘制速度方向
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(0f, height, transform.position.z), new Vector3(-1f, height - 1f, transform.position.z));
    }
}