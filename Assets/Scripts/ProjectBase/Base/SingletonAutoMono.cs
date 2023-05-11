using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  继承 mono 的 Manager 不需要手动拖或加 api
/// </summary>
public class SingletonAutoMono <T> : MonoBehaviour where T : MonoBehaviour{
    private static T instance = null;
    public static T Instance{
        get {
            // 第一次访问在场景上生成 Manager 物体
            if(instance == null){
                GameObject gameObject = new GameObject(typeof(T).ToString());
                // 过场景不移除
                GameObject.DontDestroyOnLoad(gameObject);
                instance = gameObject.AddComponent<T>();
            }
            return instance;
        }
    }
}