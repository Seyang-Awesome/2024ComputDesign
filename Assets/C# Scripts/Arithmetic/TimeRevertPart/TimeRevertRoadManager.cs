using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TimeRevertRoadManager : MonoBehaviour
{
    [SerializeField]
    private Vector3[] burstPoint;
    [SerializeField]
    float explosionForce = 10f; // 爆炸力大小
    [SerializeField]
    float explosionRadius = 5f; // 爆炸影响半径
    [SerializeField]
    float interval = 0.1f;

    private TimeRevertRoad[] roads;

    public float delay = 0.5f; // 触发间隔时间
    private bool isTriggering = false; // 是否正在触发

    private void Start()
    {
        roads = GetComponentsInChildren<TimeRevertRoad>();
        TimeRevertManager.Instance.onTrigger += OnTrigger;
        RaiseObjects();
    }

    public void OnTrigger()
    {
        if (!isTriggering)
        {
            StartCoroutine(TriggerRoads());
        }
    }

    private void RaiseObjects()
    {
        int currentIndex = 0;

        while (currentIndex < roads.Length)
        {
            roads[currentIndex + 1].transform.position += (Vector3.up * 0.1f * currentIndex);
            roads[currentIndex + 1].timer += interval * currentIndex;
            roads[currentIndex].transform.position += (Vector3.up * 0.1f * currentIndex);
            roads[currentIndex].timer += interval * currentIndex;
            currentIndex += 2;
        }
    }

    private IEnumerator TriggerRoads()
    {
        isTriggering = true;
        int numRoadsToTrigger = Mathf.CeilToInt(roads.Length / 4f); // 需要触发的道路数量
        int startIndex = 0;

        for (int i = 0; i < 4; i++)
        {
            int endIndex = startIndex + numRoadsToTrigger;
            endIndex = Mathf.Min(endIndex, roads.Length); // 防止超出数组长度

            for (int j = startIndex; j < endIndex; j++)
            {
                roads[j].Trigger(); // 触发道路的 Trigger 函数
            }
            ApplyExplosionForce(burstPoint[i]);

            yield return new WaitForSeconds(delay);
            startIndex = endIndex;
        }

        isTriggering = false;
    }


    private void ApplyExplosionForce(Vector3 position)
    {
        position += transform.position;
        Collider[] colliders = Physics.OverlapSphere(position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, position, explosionRadius);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        foreach (Vector3 point in burstPoint)
        {
            Gizmos.DrawWireSphere(point + transform.position, explosionRadius);
        }
    }
}
