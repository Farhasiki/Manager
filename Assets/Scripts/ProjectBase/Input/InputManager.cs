using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Obsolete("Not used any more", true)]
public class InputManager : BaseManager<InputManager>{
    public InputManager(){
        MonoManager.Instance.AddUpdateListener(Update);
    }

    private void CheckKeyCode(KeyCode keyCode){
        if(Input.GetKeyDown(keyCode)){
            EventCenter.Instance.EventTrigger(EventCenter.EventName.KetCodeDown,keyCode);
        }
        if(Input.GetKeyUp(keyCode)){
            EventCenter.Instance.EventTrigger(EventCenter.EventName.KeyCodeUP,keyCode);
        }
    }
    private bool isOpen = false;
    public void StartOrEndCheck(bool isOpen){
        this.isOpen = isOpen;
    }

    private void Update() {
        if(!isOpen)return ;
        CheckKeyCode(KeyCode.W);
    }
}
