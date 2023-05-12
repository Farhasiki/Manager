using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum EventName{
    MonsterDead,
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
        if(actionQueue != null) actionQueue -= action;
    }

    // 清空事件
    public void Clear(){
        eventDictionary.Clear();
    }
    private Dictionary<EventName,Action<object>> eventDictionary1 = new Dictionary<EventName, Action<object>>();
    /// <summary>
    /// 根据事件名添加事件
    /// </summary>
    public void AddEventListener(EventName eventName,Action<object> action){
        if(eventDictionary1.ContainsKey(eventName)){
            eventDictionary1[eventName] += action;
        }else{
            eventDictionary1.Add(eventName,action);
        }
    }

    /// <summary>
    /// 触发事件
    /// </summary>
    public void EventTrigger(EventName eventName, object info){
        eventDictionary1.TryGetValue(eventName,out Action<object> action);
        action?.Invoke(info);
    }
    
    /// <summary>
    /// 删除事件
    /// </summary>
    public void RemoveEventListener(EventName eventName, Action<object> action){
        eventDictionary1.TryGetValue(eventName, out Action<object> actionQueue);
        if(actionQueue != null) actionQueue -= action;
    }

    // 清空事件
    public void Clear1(){
        eventDictionary1.Clear();
    }
}