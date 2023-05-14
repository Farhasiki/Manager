using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BasePanel : MonoBehaviour{
    private Dictionary<string,List<UIBehaviour>> controlDic = new Dictionary<string, List<UIBehaviour>>();

    private void Awake() {
        FindChildrenControl<Button>();
        FindChildrenControl<Image>();
        FindChildrenControl<Toggle>();
        FindChildrenControl<Slider>();
    }
 
    protected T GetControl<T> (string name) where T : UIBehaviour{
        if(controlDic.ContainsKey(name)){
            foreach(var control in controlDic[name]){
                if(control is T){
                    return control as T;
                }
            }
        }
        return default(T);
    }

    /// <summary>
    /// 初始化存储控件
    /// </summary>
    private void FindChildrenControl<T>()where T : UIBehaviour{
        List<T> controls = this.GetComponentsInChildren<T>().ToList();
        string name;
        foreach(var control in controls){
            name = control.gameObject.name;
            if(controlDic.ContainsKey(name)){
                controlDic[name].Add(control);
            }else{
                controlDic.Add(name,new List<UIBehaviour>{control});
            }
        }
    }

    public virtual void Show(){

    }
    public virtual void Hide(){

    }
}