using System;
using UnityEngine;


public class InputTest : MonoBehaviour{
    private void Start() {
        InputManager.Instance.StartOrEndCheck(true);
        EventCenter.Instance.AddEventListener(EventCenter.EventName.KetCodeDown,KeyDown);
        EventCenter.Instance.AddEventListener(EventCenter.EventName.KeyCodeUP,KeyUp);
    }
    private void KeyDown(object key){
        Debug.Log("asd");
    }
    private void KeyUp(object key){
        Debug.Log("r");
    }
}