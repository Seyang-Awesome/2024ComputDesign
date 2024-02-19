using System;
using System.Collections.Generic;
using UnityEngine;

#if ODIN_INSPECTOR

public abstract class Publisher : MonoBehaviour, IPublisher
{
    [SerializeField] private int InitState;
    public int State { get; set; }
    public event Action<IPublisher,int,PALEventArgs> OnStateChanged;

    protected virtual void Awake()
    {
        State = InitState;
    }

    public void SetStateAndInvoke(int state,PALEventArgs args, bool ifTheSameAndCall = true)
    {
        if (state == State && !ifTheSameAndCall) return;
        State = state;
        OnStateChanged?.Invoke(this, State, args);
    }
}

#endif
