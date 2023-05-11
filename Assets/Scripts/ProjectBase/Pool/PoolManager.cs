using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : BaseManager<PoolManager>{
    public Dictionary<string,Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();
    
    /// <summary>
    /// 在对象池内获取一个物体
    /// </summary>
    /// <param name="name">对象的名称(在资源文件夹中的路径及名称)</param>
    public GameObject GetGameObject(string name){
        GameObject gameObject = null;   
        // 对象池内有当前种类对象 且 对象池内有剩余
        if(objectPool.ContainsKey(name) && objectPool[name].Count > 0){
            gameObject = objectPool[name].Dequeue();
        }else{// 创建新对象
            gameObject = GameObject.Instantiate(Resources.Load<GameObject>(name));
        }
        gameObject.SetActive(true);// 激活物体
        return gameObject;
    }

    /// <summary>
    /// 将不用的对象临时放到对象池中存储
    /// </summary>
    /// <param name="name">对象的名称(在资源文件夹中的路径及名称</param>
    /// <param name="gameObject">对象实例</param>
    public void PushGameObject(string name,GameObject gameObject){
        gameObject.SetActive(false);// 失活物体
        if(!objectPool.ContainsKey(name)){ // 创建一个新的对象池
            objectPool.Add(name,new Queue<GameObject>());
        }
        objectPool[name].Enqueue(gameObject);
    }
}