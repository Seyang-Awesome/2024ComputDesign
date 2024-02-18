using System;
using System.Collections.Generic;
using UnityEngine;

public class TestListener : Listener
{
    protected override void Awake()
    {
        base.Awake();
        stateActionDic = new()
        {
            { new Tuple<Publisher, int>(publishers[0], 0), () => Debug.Log(0) },
            { new Tuple<Publisher, int>(publishers[1], 1), () => Debug.Log(1) },
        };
    }
}

