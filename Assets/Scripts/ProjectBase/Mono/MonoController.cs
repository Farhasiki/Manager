using UnityEngine;
using System;

public class MonoController : MonoBehaviour{
    private void Start() {
        DontDestroyOnLoad(this.gameObject);
    }
    private event Action updateEvent;

    private void Update() {
        updateEvent?.Invoke();    
    } 

    /// <summary>
    /// 提供给外部的添加帧更新事件方法
    /// </summary>
    public void AddUpdateListener(Action action){
        updateEvent += action;
    }

    /// <summary>
    /// 提供给外部的移除帧更新事件方法
    /// </summary>
    public void RemoveUpdateListener(Action action){
        updateEvent -= action;
    }

}