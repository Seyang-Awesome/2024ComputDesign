using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ApexDialogBoxGroup
{
    public string content;
    public AudioClip clip;

    public float Duration => clip.length;
    
    public ApexDialogBoxGroup(string content, AudioClip clip)
    {
        this.content = content;
        this.clip = clip;
    }
}

public class ApexDialogBoxPanel : MonoSingleton<ApexDialogBoxPanel>
{
    [SerializeField] private ApexDialogBoxItem[] items;
    [SerializeField] private Transform center;
    [SerializeField] private Vector2 startPosOffset;
    [SerializeField] private Vector2 endPosOffset;
    
    private Queue<ApexDialogBoxItem> itemQueue = new();
    private Queue<ApexDialogBoxGroup> dialogBoxGroup = new();
    
    
    protected void Start()
    {
        foreach (var item in items)
        {
            itemQueue.Enqueue(item);
            item.Init();
        }
    }

    public void PushDialogContent(ApexDialogBoxGroup group)
    {
        dialogBoxGroup.Enqueue(group);
        // 说明队列中没有在显示的DialogBox
        if (itemQueue.Count == items.Length)
        {
            ShowNextContent();
        }
    }
    
    private void ShowNextContent()
    {
        ApexDialogBoxGroup group = dialogBoxGroup.Dequeue();
        itemQueue.Dequeue().Show(
            group, 
            (Vector2)center.position + startPosOffset,
            (Vector2)center.position + endPosOffset,
            OnDialogBoxItemFinished);
    }
    
    private void OnDialogBoxItemFinished(ApexDialogBoxItem item)
    {
        itemQueue.Enqueue(item);
        if (dialogBoxGroup.Count > 0)
        {
            ShowNextContent();
        }
    }
}

