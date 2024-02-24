using System;
using System.Collections.Generic;
using Sirenix.Serialization;
using Sirenix.Utilities;
using UnityEngine;

#if ODIN_INSPECTOR

[Serializable]
public struct PALCase
{
    [field: SerializeField] public List<PALCaseItem> palCaseItems { get; private set; }

    public bool IsMatchCase(Dictionary<IPublisher, int> stateDic)
    {
        if (palCaseItems.IsNullOrEmpty()) return false;
        foreach (var palCaseItem in palCaseItems)
        {
            if (!stateDic.ContainsKey(palCaseItem.Publisher)) return false;
            if (stateDic[palCaseItem.Publisher] != palCaseItem.State) return false;
        }
        
        return true;
    }
}

[Serializable]
public struct PALCaseItem
{
    [field: OdinSerialize] public IPublisher Publisher { get; private set; }
    [field: OdinSerialize] public int State { get; private set; }
}

#endif
