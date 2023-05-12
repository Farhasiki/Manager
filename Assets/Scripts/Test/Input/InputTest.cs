using System;
using UnityEngine;


public class InputTest : MonoBehaviour{
    private void Start() {
        EventCenter.Instance.AddEventListener<KeyCode>(EventName.KetCodeDown,KeyDown);
        EventCenter.Instance.AddEventListener<KeyCode>(EventName.KeyCodeUP,KeyUp);
    }
    private void KeyDown(KeyCode key){
        Debug.Log("asd");
    }
    private void KeyUp(KeyCode key){
        Debug.Log("r");
    }
}