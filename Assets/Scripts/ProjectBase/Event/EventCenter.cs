using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum EventName{
    MonsterDead,
    LoadScene,
    KetCodeDown,
    KeyCodeUP,
}
public interface IEventInfo{}

public class EventInfo<T> : IEventInfo{
    public Action<T> actions;
    public EventInfo(Action<T> action){
        actions += action;
    }
}
public class EventInfo : IEventInfo{
    public Action actions;
    public EventInfo(Action action){
        actions += action;
    }
}

public class EventCenter : BaseManager<EventCenter>{
    private Dictionary<EventName,EventHandler> eventDictionary = new Dictionary<EventName, EventHandler>();

    /// <summary>
    /// 根据事件名添加事件
    /// </summary>
    public void AddEventListener(EventName eventName,EventHandler action){
        if(eventDictionary.ContainsKey(eventName)){
            eventDictionary[eventName] += action;
        }else{
            eventDictionary.Add(eventName,action);
        }
    }

    /// <summary>
    /// 触发事件
    /// </summary>
    public void EventTrigger(EventName eventName, object sender,EventArgs info){
        eventDictionary.TryGetValue(eventName,out EventHandler action);
        action?.Invoke(sender,info);
    }
    
    /// <summary>
    /// 删除事件
    /// </summary>
    public void RemoveEventListener(EventName eventName, EventHandler action){
        eventDictionary.TryGetValue(eventName, out EventHandler actionQueue);
        actionQueue -= action;
    }

    // 清空事件
    public void Clear(){
        eventDictionary.Clear();
    }
    private Dictionary<EventName,IEventInfo> eventDictionary1 = new Dictionary<EventName, IEventInfo>();
    /// <summary>
    /// 根据事件名添加事件
    /// </summary>
    public void AddEventListener<T>(EventName eventName,Action<T> action){
        if(eventDictionary1.ContainsKey(eventName)){
            (eventDictionary1[eventName] as EventInfo<T>).actions += action;
        }else{
            eventDictionary1.Add(eventName,new EventInfo<T>(action));
        }
    }
    public void AddEventListener(EventName eventName,Action action){
        if(eventDictionary1.ContainsKey(eventName)){
            (eventDictionary1[eventName] as EventInfo).actions += action;
        }else{
            eventDictionary1.Add(eventName,new EventInfo(action));
        }
    }

    /// <summary>
    /// 触发事件
    /// </summary>
    public void EventTrigger<T>(EventName eventName, T info){
        if(eventDictionary1.TryGetValue(eventName,out IEventInfo action)){
            (action as EventInfo<T>).actions?.Invoke(info);
        }
    }
    public void EventTrigger(EventName eventName){
        if(eventDictionary1.TryGetValue(eventName,out IEventInfo action)){
            (action as EventInfo).actions?.Invoke();
        }
    }
    
    /// <summary>
    /// 删除事件
    /// </summary>
    public void RemoveEventListener<T>(EventName eventName, Action<T> action){
        if(eventDictionary1.TryGetValue(eventName, out IEventInfo actionQueue)){
            (actionQueue as EventInfo<T>).actions -= action;
        }
    }
    public void RemoveEventListener(EventName eventName, Action action){
        if(eventDictionary1.TryGetValue(eventName, out IEventInfo actionQueue)){
            (actionQueue as EventInfo).actions -= action;
        }
    }

    // 清空事件
    public void Clear1(){
        eventDictionary1.Clear();
    }
}