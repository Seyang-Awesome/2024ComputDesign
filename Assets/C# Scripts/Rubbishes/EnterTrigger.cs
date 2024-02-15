using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTrigger : Switch
{
    [SerializeField]
    private IAnimatable animateItem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            SwitchOn();
    }
}
