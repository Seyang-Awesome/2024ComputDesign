using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IEventInfo
{
    
}

public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> actions;
    public EventInfo(UnityAction<T> action)
    {
        actions += action;
    }
}

public  class EventInfo : IEventInfo
{
    public UnityAction actions;

    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}

/// <summary>
/// 事件中心
/// 采用单例与观察者模式
/// </summary>
public class EventCenter : BaseManager<EventCenter>
{
    //Key:事件名称
    //Value:监听该事件的委托方法
    
    private Dictionary<string, IEventInfo> dicEvent = new Dictionary<string, IEventInfo>();

    /// <summary>
    /// 添加监听
    /// </summary>
    /// <param name="name">事件名</param>
    /// <param name="action">委托方法</param>
    public void AddEventListener<T>(string name, UnityAction<T> action)
    {
        if (dicEvent.ContainsKey(name))
        {
            (dicEvent[name] as EventInfo<T>).actions += action;
        }
        else
        {
            dicEvent.Add(name, new EventInfo<T>(action));
        }
    }
    /// <summary>
    /// 添加监听(不需要参数)
    /// </summary>
    /// <param name="name">事件名</param>
    /// <param name="action">委托方法</param>
    public void AddEventListener(string name, UnityAction action)
    {
        if (dicEvent.ContainsKey(name))
        {
            (dicEvent[name] as EventInfo).actions += action;
        }
        else
        {
            dicEvent.Add(name, new EventInfo(action));
        }
    }

    /// <summary>
    /// 移除监听
    /// </summary>
    /// <param name="name">事件名</param>
    /// <param name="action">委托方法</param>
    public void RemoveEventListener<T>(string name, UnityAction<T> action)
    {
        if (dicEvent.ContainsKey(name))
        {
            (dicEvent[name] as EventInfo<T>).actions -= action;
        }
    }
    /// <summary>
    /// 移除监听(不需要参数)
    /// </summary>
    /// <param name="name">事件名</param>
    /// <param name="action">委托方法</param>
    public void RemoveEventListener(string name, UnityAction action)
    {
        if (dicEvent.ContainsKey(name))
        {
            (dicEvent[name] as EventInfo).actions -= action;
        }
    }

    /// <summary>
    /// 事件触发
    /// </summary>
    /// <param name="name">触发的事件</param>
    /// <param name="info">传入的参数</param>
    public void EventTrigger<T>(string name,T info)
    {
        if (dicEvent.ContainsKey(name))
        {

                (dicEvent[name] as EventInfo<T>).actions?.Invoke(info);
            
            //dicEvent[name].Invoke(info);
            ////dicEvent[name]();
        }
    }
    /// <summary>
    /// 事件触发(不需要参数)
    /// </summary>
    /// <param name="name">触发的事件</param>
    public void EventTrigger(string name)
    {
        if (dicEvent.ContainsKey(name))
        {
            (dicEvent[name] as EventInfo).actions?.Invoke();
        }
    }

    /// <summary>
    /// 清空
    /// </summary>
    public void Clear()
    {
        dicEvent.Clear();
    }
}
