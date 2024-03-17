using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeInfo
{
    public Vector3 pos;
    public Quaternion rot;

    public TimeInfo(Vector3 pos, Quaternion rot)
    {
        this.pos = pos;
        this.rot = rot;
    }
}
public class TimeRevertRoad : MonoBehaviour
{
    public float timer = 3;

    private Stack<TimeInfo> records = new Stack<TimeInfo>();
    private Rigidbody rb;
    private bool isTrigger = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    public void Trigger()
    {
        rb.isKinematic = false;
        isTrigger = true;
    }

    private void Update()
    {
        if (!isTrigger)
            return;
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            records.Push(new TimeInfo(transform.position, transform.rotation));
        }

        if(TimeRevertManager.Instance.isRevert && records.Count > 0)
        {
            var info = records.Pop();
            transform.position = info.pos;
            transform.rotation = info.rot;
        }

        if(records.Count == 0)
        {
            rb.isKinematic = true;
        }
    }
}
