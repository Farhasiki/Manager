using XLua;
using System.IO;
using UnityEngine;
/// <summary>
/// lua 管理器
/// 提供 lua 管理器
/// 保证解析器的唯一性
/// 执行 LuaWorkSpace 文件夹下的 lua 脚本
/// 导入 xlua 文件
/// </summary>

public class LuaManager : BaseManager<LuaManager>{
    private const string LUA_WORKSPACE = "/LuaWorkSpace/";
    // 执行 lua 脚本
    // 释放垃圾
    // 销毁解析器
    // 重定向文件
    private LuaEnv luaEnv;

    public LuaTable Global{
        get{
            return luaEnv.Global;
        }
    }

    /// <summary>
    /// 初始化解析器
    /// </summary>
    public void Init(){
        if(luaEnv != null)return ;

        luaEnv = new LuaEnv();
        // 文件重定向
        //luaEnv.AddLoader(MyCostomLoader);
        luaEnv.AddLoader(LoadFromAssetBundle);
    }

    public void DoLuaFile(string fileName){
        if(luaEnv == null)Debug.LogError("解析器未初始化");
        string command = string.Format("require('{0}')",fileName);
        DoString(command);
    }

    /// <summary>
    /// 执行 lua 语言
    /// </summary>
    public void DoString(string command){
        if(luaEnv == null)Debug.LogError("解析器未初始化");
        luaEnv.DoString(command);
    } 

    /// <summary>
    /// 释放垃圾
    /// </summary>
    public void Tick(){
        if(luaEnv == null)Debug.LogError("解析器未初始化");
        luaEnv.Tick();
    }

    /// <summary>
    /// 销毁 lua 解析器
    /// </summary>
    public void Dispose(){
        if(luaEnv == null)Debug.LogError("解析器未初始化");
        luaEnv.Dispose();
        luaEnv = null;
    }

    /// <summary>
    /// 判断 lua 解析器是否初始化
    /// </summary>
    /// <returns></returns>
    public bool IsInit(){
        return luaEnv != null;
    }

    /// <summary>
    /// 文件重定向函数
    /// </summary>
    private byte[] MyCostomLoader(ref string fileName){
        // 拼接一个文件路径
        string path = FormatLuaPath(Application.dataPath,fileName);
        
        if(File.Exists(path)){
            return File.ReadAllBytes(path);
        }else{
            Debug.Log("MyCostomLoader 重定向失败,文件名为" + fileName);            
            return null;
        }
    }
   
    // 脚本放在 AssetBundle 包中
    // 通过加载 AssetBundle 中的资源再加载 lua 脚本来执行
    /// <summary>
    /// 重定向到 AssetBundle 文件中
    /// </summary>
    private byte[] LoadFromAssetBundle(ref string fileName){
        // 获取文件资源
        TextAsset textAsset = AssetBundleMgr.Instance.LoadResources<TextAsset>("lua",fileName + ".lua");
        if(textAsset == null) {
            Debug.Log("LoadFromAssetBundle 重定向失败，文件名为" + fileName);
            return null;
        }
        return textAsset.bytes;
    }

    /// <summary>
    /// 拼接 lua脚本 文件路径
    /// </summary>
    private string FormatLuaPath(string dataPath,string fileName){
        return dataPath + LUA_WORKSPACE + fileName + ".lua";
    }
}
