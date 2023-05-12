using System.Runtime.Versioning;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 资源加载管理器
/// </summary>
public class ResManager : BaseManager<ResManager>{
    public T Load<T> (string resName) where T : Object{
        T res = Resources.Load<T> (resName);
        // 如果是 GameObject 直接实例化后返回
        if(res is GameObject) 
            return GameObject.Instantiate(res);
        return res;
    } 

    public void LoadAsync<T>(string resName,System.Action<T> callback) where T : Object{
        MonoManager.Instance.StartCoroutine(ReallLoadAsync<T>(resName,callback));
    }

    private IEnumerator ReallLoadAsync<T>(string resName, System.Action<T> callback)where T : Object{
        ResourceRequest resourceRequest = Resources.LoadAsync<T>(resName);
        yield return resourceRequest;

        if(resourceRequest.asset is GameObject){
            callback(GameObject.Instantiate(resourceRequest.asset) as T);
        }else{
            callback(resourceRequest.asset as T);
        }
    }
}
