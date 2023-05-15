using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 导入AB包后使用 AssetBundleManager 加载资源
/// 加载前先初始化
/// </summary>
public class AssetBundleMgr : SingletonAutoMono<AssetBundleMgr>{
    private  AssetBundle mainAssetBundle = null;
    private AssetBundleManifest assetBundleManifest = null;
    private string PathUrl{
        get{
            return Application.streamingAssetsPath + '/';
        }
    }
    private string mainAssetBundleName{
        get{
            #if UNITY_iOS
                return "iOS";
            #elif UNITY_ANDROID
                return "Android";
            #else 
                return "PC";
            #endif
        }
    }

    private Dictionary<string,AssetBundle> assetBundleDic = new Dictionary<string, AssetBundle>();


    //同步加载
    public Object LoadResources(string assetbundleName, string resourcesName){
        LoadAssetbundle(assetbundleName);
        Object resourcesObject = assetBundleDic[assetbundleName].LoadAsset(resourcesName);
        return resourcesObject;
    }
    /// <summary>
    /// 类型加载资源
    /// </summary>
    public Object LoadResources(string assetbundleName, string resourcesName, System.Type type){
        LoadAssetbundle(assetbundleName);
        Object resourcesObject = assetBundleDic[assetbundleName].LoadAsset(resourcesName,type);
        return resourcesObject;
    }

    //泛型加载
    public T LoadResources<T>(string assetbundleName, string resourcesName)where T : Object{
        LoadAssetbundle(assetbundleName);
        T resourcesObject = assetBundleDic[assetbundleName].LoadAsset<T>(resourcesName);
        return resourcesObject;
    }
    public void LoadAssetbundle(string assetbundleName){
        if(mainAssetBundle == null){
            mainAssetBundle = AssetBundle.LoadFromFile(PathUrl + mainAssetBundleName);
            assetBundleManifest = mainAssetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
        List<string> dependences = assetBundleManifest.GetAllDependencies(assetbundleName).ToList();
        foreach(var dependent in dependences){
            TryToAddAssetBundle(dependent);
        }
        TryToAddAssetBundle(assetbundleName);
    }
    private void TryToAddAssetBundle(string assetbundleName){
        if(!assetBundleDic.ContainsKey(assetbundleName)){
            assetBundleDic.Add(assetbundleName,AssetBundle.LoadFromFile(PathUrl + assetbundleName));
        }
    }

    /// <summary>
    /// 卸载资源
    /// </summary>
    public bool UnloadResources(string assetbundleName,bool unloadAllLoadedObjects = false){
        assetBundleDic.TryGetValue(assetbundleName,out AssetBundle assetBundle);
        if(assetBundle != null){
            assetBundle.Unload(unloadAllLoadedObjects);
            assetBundleDic.Remove(assetbundleName);
            return true;
        }
        return false;
    }

    public void ClearAsstbundle(){
        AssetBundle.UnloadAllAssetBundles(false);
        assetBundleDic.Clear();
        mainAssetBundle.Unload(false);
        assetBundleManifest = null;
    } 

    /// <summary>
    /// 异步加载资源
    /// </summary>
    public void LoadResourcesAsync(string assetbundleName, string resourcesName,System.Action<Object> callback){
        StartCoroutine(ReallyLoadResourcesAsync(assetbundleName,resourcesName,callback));
    }
    private IEnumerator ReallyLoadResourcesAsync(string assetbundleName, string resourcesName,System.Action<Object> callback){
        LoadAssetbundle(assetbundleName);
        AssetBundleRequest assetBundleRequest = assetBundleDic[assetbundleName].LoadAssetAsync(resourcesName);
        yield return assetBundleRequest;
        callback(assetBundleRequest.asset);
    }
    /// <summary>
    /// 异步加载资源
    /// </summary>
    public void LoadResourcesAsync(string assetbundleName, string resourcesName,System.Type type,System.Action<Object> callback){
        StartCoroutine(ReallyLoadResourcesAsync(assetbundleName,resourcesName,type,callback));
    }
    private IEnumerator ReallyLoadResourcesAsync(string assetbundleName, string resourcesName,System.Type type,System.Action<Object> callback){
        LoadAssetbundle(assetbundleName);
        AssetBundleRequest assetBundleRequest = assetBundleDic[assetbundleName].LoadAssetAsync(resourcesName,type);
        yield return assetBundleRequest;
        callback(assetBundleRequest.asset);
    }
    /// <summary>
    /// 异步加载资源
    /// </summary>
    public void LoadResourcesAsync<T>(string assetbundleName, string resourcesName,System.Action<T> callback) where T : Object{
        StartCoroutine(ReallyLoadResourcesAsync<T>(assetbundleName,resourcesName,callback));
    }
    private IEnumerator ReallyLoadResourcesAsync<T>(string assetbundleName, string resourcesName,System.Action<T> callback) where T : Object{
        LoadAssetbundle(assetbundleName);
        AssetBundleRequest assetBundleRequest = assetBundleDic[assetbundleName].LoadAssetAsync<T>(resourcesName);
        yield return assetBundleRequest;
        callback(assetBundleRequest.asset as T);
    }
}