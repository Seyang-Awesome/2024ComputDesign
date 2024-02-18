using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Publisher : MonoBehaviour
{
    [SerializeField] private int InitState;
    public int State { get; private set; }
    public event Action<Publisher,int> OnStateChanged;

    protected virtual void Awake()
    {
        State = InitState;
    }

    protected void SetStateAndInvoke(int state)
    {
        if (state == State) return;
        State = state;
        OnStateChanged?.Invoke(this, State);
    }
}

