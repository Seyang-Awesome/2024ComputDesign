using System;
using System.Collections.Generic;
using UnityEngine;

public class TestPublisher1 : Publisher
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            SetStateAndInvoke(1);
    }
}

