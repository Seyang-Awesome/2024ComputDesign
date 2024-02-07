using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineAnimation : MonoBehaviour
{
    [SerializeField]
    private float aheadOffset = 30;
    [SerializeField]
    private float maxLength = 200;
    [SerializeField]
    private LineRenderer[] axises;

    private float currentLength = 0;

    private bool isAwake = false;

    private void Awake()
    {
        foreach (var line in axises)
        {
            line.SetPosition(1, Vector3.zero);
            line.transform.GetChild(0).localPosition = Vector3.down * 1000;
        }
    }

    public void SetAwake()
    {
        isAwake = true;
    }

    private void Update()
    {
        if(isAwake)
            UpdateLength();
    }

    private void UpdateLength()
    {
        var p = Player.Instance.transform.position;
        var len = p.x - transform.position.x + aheadOffset;

        currentLength = Mathf.MoveTowards(currentLength, len, Time.deltaTime * 10);
        
        foreach(var line in axises)
        {
            line.SetPosition(1,Vector3.right * currentLength);
            line.transform.GetChild(0).localPosition = Vector3.right * currentLength;
        }
    }
}
