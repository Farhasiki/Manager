using System.IO;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 池内单类数据
/// </summary>
public class PoolData{
    public Transform fatherRoot;
    public Queue<GameObject> poolQueue;
    public PoolData (GameObject gameObject, Transform poolRoot){
        fatherRoot = new GameObject(gameObject.name).transform;
        fatherRoot.parent = poolRoot;
        poolQueue = new Queue<GameObject>();
    }
    public GameObject GetGameObject(){
        GameObject gameObject = poolQueue.Dequeue();   
        gameObject.SetActive(true);// 激活物体
        gameObject.transform.parent = null;
        return gameObject;
    }

    public void PushGameObject(GameObject gameObject){
        gameObject.transform.parent = fatherRoot;
        gameObject.SetActive(false);// 失活物体
        poolQueue.Enqueue(gameObject);
    }
}

public class PoolManager : BaseManager<PoolManager>{
    public Dictionary<string,PoolData> objectPool = new Dictionary<string, PoolData>();
    private Transform poolRoot;
    /// <summary>
    /// 在对象池内获取一个物体
    /// </summary>
    /// <param name="name">对象的名称(在资源文件夹中的路径及名称)</param>
    public GameObject GetGameObject(string name){
        GameObject gameObject = null;   
        // 对象池内有当前种类对象 且 对象池内有剩余
        if(objectPool.ContainsKey(name) && objectPool[name].poolQueue.Count > 0){
            return gameObject = objectPool[name].GetGameObject();
        }else{// 创建新对象
            gameObject = GameObject.Instantiate(Resources.Load<GameObject>(name));
            gameObject.name = name;
        }
        return gameObject;
    }

    /// <summary>
    /// 异步获取资源
    /// </summary>
    /// <param name="name">资源名称</param>
    public void GetGameObjectAsync(string name, System.Action<GameObject> callback){
        if(objectPool.ContainsKey(name) && objectPool[name].poolQueue.Count > 0){
            callback?.Invoke(objectPool[name].GetGameObject());
        }else{// 创建新对象
            ResManager.Instance.LoadAsync<GameObject>(name,(obj)=>{
                obj.name = name;
                callback?.Invoke(obj);
            });
        }
    }

    /// <summary>
    /// 将不用的对象临时放到对象池中存储
    /// </summary>
    /// <param name="name">对象的名称(在资源文件夹中的路径及名称</param>
    /// <param name="gameObject">对象实例</param>
    public void PushGameObject(GameObject gameObject){
        if(poolRoot == null)
            poolRoot = new GameObject("Pool").transform;
        gameObject.transform.parent = poolRoot;
        gameObject.SetActive(false);// 失活物体
        if(!objectPool.ContainsKey(gameObject.name)){ // 创建一个新的对象池
            objectPool.Add(gameObject.name,new PoolData(gameObject,poolRoot));
        }
        objectPool[gameObject.name].PushGameObject(gameObject);
    }

    public void Clear(){
        objectPool.Clear();
        poolRoot = null;
    }
}