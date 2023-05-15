using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class LuaCopyEditor : Editor{
    // 在 XLua 的菜单条上添加 Item 
    [MenuItem("XLua/Copy lua to txt")]
    /// <summary>
    /// 把 LuaWorkSpace 文件夹内的 lua 文件打包成 txt 格式的文件到 LuaTxt 文件夹
    /// </summary>
    public static void CopyLuaToTxt(){
        // 获取所有 lua 文件
        string path = Application.dataPath + "/LuaWorkSpace/";
        // 判断文件是否存在
        if(!Directory.Exists(path))return ;
        // 获取每个 lua 文件的路径
        string[] strs = Directory.GetFiles(path,"*.lua");

        // 把文件放到新的文件夹中
        string newPath = Application.dataPath + "/LuaTxt";
        // 判断文件夹是否存在
        if(Directory.Exists(newPath)){
            Directory.Delete(newPath,true);
        }
        Directory.CreateDirectory(newPath);
        // 复制文件
        string filePath;
        List<string> newFilesName = new List<string>();
        for(int idx = 0; idx < strs.Length; ++idx){
            filePath = newPath + strs[idx].Substring(strs[idx].LastIndexOf('/')) + ".txt";
            File.Copy(strs[idx],filePath);
            newFilesName.Add(filePath);
        }

        // 刷新文件夹
        AssetDatabase.Refresh();

        // 刷新后改打包名称
        foreach(var fileName in newFilesName){
            // Unity API
            // 该 API 传入的路径必须是以 Assets 开头
            AssetImporter assetImporter = AssetImporter.GetAtPath(fileName.Substring(fileName.IndexOf("Assets")));
            if(assetImporter != null){
                assetImporter.assetBundleName = "lua";
            }
        }
    }
}