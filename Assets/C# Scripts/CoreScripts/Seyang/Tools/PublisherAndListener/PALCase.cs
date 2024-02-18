using System;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

#if ODIN_INSPECTOR

[Serializable]
public class PALCase
{
    [field: SerializeField] public List<PALCaseItem> palCaseItems { get; private set; }

    public bool IsMatchCase(Dictionary<Publisher, int> stateDic)
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
public class PALCaseItem
{
    [field: SerializeField] public Publisher Publisher { get; private set; }
    [field: SerializeField] public int State{ get; private set; }
}

#endif
