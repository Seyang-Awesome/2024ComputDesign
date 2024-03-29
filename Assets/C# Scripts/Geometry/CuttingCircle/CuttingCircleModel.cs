using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class CuttingCircleModel : SerializedMonoBehaviour
{
    // 具体数据在Inspector面板上看
    [OdinSerialize] private Queue<CuttingCircleData> modelQueue = new();
    public CuttingCircleData CurrentModel { get; private set; }
    public event Action<CuttingCircleData> OnCurrentModelChanged;
    public event Action OnLastModelDequeue;
    private void Start()
    {
        MoveNext();
    }

    public void MoveNext()
    {
        if (modelQueue.Count == 0) return;
        CurrentModel = modelQueue.Dequeue();
        OnCurrentModelChanged?.Invoke(CurrentModel);
        
        if(modelQueue.Count == 0)
            OnLastModelDequeue?.Invoke();
    }
}

[Serializable]
public class CuttingCircleData
{
    public float area;
    public int lineNum;
}



