using System;
using System.Collections.Generic;
using UnityEngine;

#if ODIN_INSPECTOR

public abstract class Publisher : MonoBehaviour
{
    [SerializeField] private int InitState;
    public int State { get; private set; }
    public event Action<Publisher,int,PALEventArgs> OnStateChanged;

    protected virtual void Awake()
    {
        State = InitState;
    }

    protected void SetStateAndInvoke(int state,PALEventArgs args, bool ifTheSameAndCall = true)
    {
        if (state == State && !ifTheSameAndCall) return;
        State = state;
        OnStateChanged?.Invoke(this, State, args);
    }
}

#endif
