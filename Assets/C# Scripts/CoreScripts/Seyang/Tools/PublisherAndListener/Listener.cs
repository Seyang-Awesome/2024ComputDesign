#if ODIN_INSPECTOR

using System;
using System.Collections.Generic;
using Sirenix.Serialization;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public abstract class Listener : SerializedMonoBehaviour 
{
    //通过Inspector窗口挂载相应的监听者
    [field: SerializeField,
            LabelText("订阅者列表")]
    protected List<Publisher> publishers { get; private set; }
    
    //通过Inspector窗口设置特定情况下发生的事件
    [OdinSerialize,
     DictionaryDrawerSettings(KeyLabel = "触发情况", ValueLabel = "触发动作"),
     LabelText("特定情况下的事件")] 
    private Dictionary<PALCase, UnityAction> stateActionDic = new();
    
    //在其他情况下发生的事件
    [OdinSerialize,
     LabelText("其他情况下的事件")] 
    private UnityAction elseAction;
    
    //当前的Publisher的状态
    private Dictionary<Publisher, int> currentStateDic = new(); 
    
    protected virtual void Awake()
    {
        if (publishers.IsNullOrEmpty())
        {
            publishers = new();
            return;
        }
        publishers.ForEach(publisher =>
        {
            publisher.OnStateChanged += OnPublisherStateChanged;
            currentStateDic.Add(publisher, publisher.State);
        });
    }

    private void OnDestroy()
    {
        publishers.ForEach(publisher => publisher.OnStateChanged -= OnPublisherStateChanged);
    }

    private void OnPublisherStateChanged(Publisher publisher, int state)
    {
        if (!currentStateDic.ContainsKey(publisher)) return;
        currentStateDic[publisher] = state;

        UnityAction action = GetRelevantAction();
        if(action == null)
            elseAction?.Invoke();
        else
            action.Invoke();
    }

    private UnityAction GetRelevantAction()
    {
        foreach (var stateAction in stateActionDic)
        {
            if (stateAction.Key.IsMatchCase(currentStateDic))
                return stateAction.Value;
        }
        
        return null;
    }
}

#endif


