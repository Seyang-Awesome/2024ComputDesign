using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingItem : MonoBehaviour
{
    [SerializeField]
    private Vector3 moveDir = Vector3.zero;

    [SerializeField]
    private float rotateSpeed = 0;

    private Vector3 rotateAxis;

    private void Start()
    {
        rotateAxis = Random.insideUnitSphere;
    }

    private void Update()
    {
        transform.Translate(moveDir * Time.deltaTime);
        transform.Rotate(rotateAxis * rotateSpeed * Time.deltaTime);
    }
}
