using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TipSpirit : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Vector3[] points;

    private int index = 0;
    private bool playerTriggered = true;

    private void Start()
    {
        points = transform.Find("Points").GetComponentsInChildren<Transform>().Select(p => p.position).ToArray();
        transform.position = points[index];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && playerTriggered)
        {
            TipUIManager.Instance.UpdateTip(index++);
            playerTriggered = false;
        }
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, points[index]) < 0.01f)
        {
            playerTriggered = true;
        }

        transform.position = Vector3.MoveTowards(transform.position, points[index], speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        var points = transform.Find("Points").GetComponentsInChildren<Transform>();
        Gizmos.color = Color.green;
        for (int i = 0; i < points.Length; i++)
        {
            Gizmos.DrawWireSphere(points[i].position, 1f);
        }
        for (int i = 0; i < points.Length - 1; i++)
        {
            Gizmos.DrawLine(points[i].position, points[i + 1].position);
        }
    }
}
