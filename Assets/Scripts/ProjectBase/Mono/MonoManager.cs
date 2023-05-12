using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Internal;

/// <summary>
/// 提供给外部添加帧更新方法
/// 提供给外部添加携程的方法
/// </summary>
public class MonoManager : BaseManager<MonoManager>{
    private MonoController monoController;
    public MonoManager(){
        // 保证 Monocontroller 的唯一性
        monoController = new GameObject("MonoController").AddComponent<MonoController>();
    }

    /// <summary>
    /// 提供给外部的添加帧更新事件方法
    /// </summary>
    public void AddUpdateListener(Action action){
        monoController.AddUpdateListener(action);
    }

    /// <summary>
    /// 提供给外部的移除帧更新事件方法
    /// </summary>
    public void RemoveUpdateListener(Action action){
        monoController.RemoveUpdateListener(action);
    }

    public Coroutine StartCoroutine(string methodName){
        return monoController.StartCoroutine(methodName);
    }
    public Coroutine StartCoroutine(IEnumerator routine){
        return monoController.StartCoroutine(routine);
    }
    public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value){
        return monoController.StartCoroutine(methodName,value);
    }
    public void StopCoroutine(IEnumerator routine){
        monoController.StopCoroutine(routine);
    }
    public void StopCoroutine(Coroutine routine){
        monoController.StopCoroutine(routine);
    }
    public void StopCoroutine(string methodName){
        monoController.StopCoroutine(methodName);
    }
    public void StopAllCoroutines(){
        monoController.StopAllCoroutines();
    }
}