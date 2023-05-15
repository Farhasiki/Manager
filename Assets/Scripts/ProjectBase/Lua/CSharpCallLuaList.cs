using System;
using System.Collections.Generic;
using XLua;
using UnityEngine.Events;

/// <summary>
/// 系统类型统一加特性
/// </summary>
public static class CSharpCallLuaList{
    [CSharpCallLua]
    public static List<Type> csharpCallLuaList = new List<Type>{
        typeof(UnityAction<bool>)
    };
}