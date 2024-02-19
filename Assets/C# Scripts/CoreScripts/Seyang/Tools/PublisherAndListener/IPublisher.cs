using System;
using System.Collections.Generic;
using UnityEngine;

#if ODIN_INSPECTOR

public interface IPublisher
{
    public int State { get; set; }
    public event Action<IPublisher,int,PALEventArgs> OnStateChanged;
    public void SetStateAndInvoke(int state, PALEventArgs args, bool ifTheSameAndCall = true);
}

#endif


