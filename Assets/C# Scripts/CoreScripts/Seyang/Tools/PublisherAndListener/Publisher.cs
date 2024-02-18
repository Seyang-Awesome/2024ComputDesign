using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Publisher : MonoBehaviour
{
    public int State { get; private set; }
    public event Action<Publisher,int> OnStateChanged;

    protected void SetStateAndInvoke(int state)
    {
        State = state;
        OnStateChanged?.Invoke(this, State);
    }
}

