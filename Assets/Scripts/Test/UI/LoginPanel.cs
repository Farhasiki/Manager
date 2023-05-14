using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginPanel : BasePanel{
    protected override void Awake()
    {
        base.Awake();
    }
    public void init(){
        Debug.Log("A");
    }
    protected override void OnClick(string ButtonName)
    {
        base.OnClick(ButtonName);
    }
}
