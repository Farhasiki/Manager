using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 因为要挂载到场景对象上 所以需要手动保证他的唯一性
/// </summary>
public class SingletonMonoBase<T> : MonoBehaviour where T : MonoBehaviour{
    public static T Instance {get;private set;}
    // 继承了 mono 的类不需要 new 实例
    // 通过在场景上挂载时生成对象
    // unity 自动生成
    protected virtual void Awake() {
        Instance = this as T;
    }
}