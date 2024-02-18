using System;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

public abstract class Listener : MonoBehaviour
{
    [field: SerializeField] protected List<Publisher> publishers { get; private set; }
    private Dictionary<Publisher, int> currentStateDic = new();
    protected Dictionary<Tuple<Publisher, int>, Action> stateActionDic;
    
    protected virtual void Awake()
    {
        if (publishers.IsNullOrEmpty()) return;
        publishers.ForEach(publisher =>
        {
            publisher.OnStateChanged += OnStateChangedSingle;
            currentStateDic.Add(publisher, publisher.State);
        });
    }

    private void OnDestroy()
    {
        publishers.ForEach(publisher => publisher.OnStateChanged -= OnStateChangedSingle);
    }

    private void OnStateChangedSingle(Publisher publisher, int state)
    {
        if (!currentStateDic.ContainsKey(publisher)) return;
        currentStateDic[publisher] = state;

        Action action = CompareStateDic(publisher);
        action.Invoke();
    }

    private Action CompareStateDic(Publisher publisher)
    {
        foreach (var stateAction in stateActionDic)
        {
            if (stateAction.Key.Item1 == publisher && 
                stateAction.Key.Item2 == currentStateDic[publisher])
                return stateAction.Value;
        }

        return null;
    }
}

