using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

/// <summary>
/// 面板层级
/// </summary>
public enum E_UI_Layer{
    Bot,
    Mid,
    Top,
    System,
}

/// <summary>
/// 面板管理器
/// 1.管理所有显示的面板
/// 2.为外部提供显示和隐藏等等接口
/// </summary>
public class UIManager : BaseManager<UIManager>{
    private const string CANVAS_RESOURCES = "UI/Canvas";
    private const string EVENTSYSTEM_RESOURCES = "UI/EventSystem";
    private const string UI_RESOURCES = "UI/";
    public Transform canvas;
    private Dictionary<E_UI_Layer, Transform> UI_Layer = new Dictionary<E_UI_Layer, Transform>();
    private  Dictionary<string,BasePanel> panelDictionary = new Dictionary<string, BasePanel>();
    public UIManager(){
        // 加载不删除
        canvas = ResManager.Instance.Load<GameObject>(CANVAS_RESOURCES).transform;
        GameObject.DontDestroyOnLoad(canvas);

        // 各个层级
        UI_Layer.Add(E_UI_Layer.Mid,canvas.Find("Mid"));
        UI_Layer.Add(E_UI_Layer.Bot,canvas.Find("Bot"));
        UI_Layer.Add(E_UI_Layer.Top,canvas.Find("Top"));
        UI_Layer.Add(E_UI_Layer.System,canvas.Find("System"));

        // 加载 EventSystem 
        GameObject eventSystem = ResManager.Instance.Load<GameObject>(EVENTSYSTEM_RESOURCES);
        GameObject.DontDestroyOnLoad(eventSystem);
    }

    /// <summary>
    /// 显示面板(因为是异步加载所以想要让面板创建后做的事情 在回调函数内执行)
    /// 面板名字和脚本名一致
    /// </summary>
    /// <param name="layer">面板层级</param>
    /// <param name="action">面板创建后做的事</param>
    public void ShowPanel<T>(System.Action<T> callback = null,E_UI_Layer layer = E_UI_Layer.Mid)where T : BasePanel{
        string panelName = typeof(T).ToString();
        if(panelDictionary.ContainsKey(panelName)){
            panelDictionary[panelName].Show();
            callback?.Invoke(panelDictionary[panelName] as T);
            return ;
        }
        ResManager.Instance.LoadAsync<GameObject>(UI_RESOURCES + panelName,(gameObject)=>{
            if(UI_Layer.ContainsKey(layer)){
                Transform father = UI_Layer[layer];
                // 设置父对象 设置相对位置大小
                gameObject.transform.SetParent(father,false);
                gameObject.transform.localPosition = Vector3.zero;
                gameObject.transform.localScale = Vector3.one;
                gameObject.transform.name = panelName;
                (gameObject.transform as RectTransform).offsetMax = Vector2.zero;
                (gameObject.transform as RectTransform).offsetMin = Vector2.zero;
            }
            T panel = gameObject.GetComponent<T>();
            if(panel == null)Debug.LogError("未挂载组件");
            panel.Show();
            callback?.Invoke(panel);

            // 记录生成的面板
            panelDictionary.Add(panelName,panel);
        });
    }


    /// <summary>
    /// 关闭面板
    /// </summary>
    public void HidePanel<T>() where T : BasePanel{
        string panelName = typeof(T).ToString();
        if(panelDictionary.ContainsKey(panelName)){
            GameObject.Destroy(panelDictionary[panelName].gameObject);
            panelDictionary.Remove(panelName);
        }
    }

    /// <summary>
    /// 获取面板
    /// </summary>
    public T GetPanel<T>()where T : BasePanel{
        string panelName = typeof(T).ToString();
        if(panelDictionary.ContainsKey(panelName)){
            return panelDictionary[panelName] as T;
        }
        return default(T);
    }
    public Transform GetLayer(E_UI_Layer layer){
        return UI_Layer[layer]; 
    }

    /// <summary>
    /// 控件添加自定义事件监听
    /// </summary>
    public void AddCustomEventListener(UIBehaviour control,EventTriggerType type,UnityAction<BaseEventData> callback){
        EventTrigger trigger = control.GetComponent<EventTrigger>();
        if(trigger == null) control.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = type;
        entry.callback.AddListener(callback);

        trigger.triggers.Add(entry);
    }
}