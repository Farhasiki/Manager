using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUI : MonoBehaviour{
    void Start()
    {
        UIManager.Instance.ShowPanel<LoginPanel>((loginPanel)=>{
            loginPanel.init();
        });
    }
}
