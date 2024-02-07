using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTrigger : MonoBehaviour
{
    [SerializeField]
    private TimeLineAnimation timeLine;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            timeLine.SetAwake();
    }
}
