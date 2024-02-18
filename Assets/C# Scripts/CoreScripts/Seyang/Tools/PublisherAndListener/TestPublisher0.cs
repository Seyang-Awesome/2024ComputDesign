using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestPublisher0 : Publisher
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
            SetStateAndInvoke(0);
    }
}

