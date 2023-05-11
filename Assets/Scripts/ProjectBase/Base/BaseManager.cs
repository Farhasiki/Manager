using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager<T> where T : new() {
    // 加载类时实例化 Manager 对象
    private static T instance = new T();
    public static T Instance => instance;
}
