using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BasePanel : MonoBehaviour{
    private Dictionary<string,List<UIBehaviour>> controlDic = new Dictionary<string, List<UIBehaviour>>();

    protected virtual void Awake() {
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
        foreach(var control in controls){
            var name = control.gameObject.name;
            if(controlDic.ContainsKey(name)){
                controlDic[name].Add(control);
            }else{
                controlDic.Add(name,new List<UIBehaviour>{control});
            }
            if(control is Button){
                (control as Button).onClick.AddListener(() => {
                    OnClick(name);
                });
            }else if (control is Toggle){
                (control as Toggle).onValueChanged.AddListener((value) => {
                    OnValueChange(name,value);
                });
            }
        }
    }
    protected virtual void OnClick(string buttonName){}
    protected virtual void OnValueChange(string toggleName,bool value){}
    public virtual void Show(){

    }
    public virtual void Hide(){

    }
}