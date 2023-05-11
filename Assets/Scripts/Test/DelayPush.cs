using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayPush : MonoBehaviour{
    private void OnEnable() {
        Invoke("Push",1);
    }

    private void Push(){
        PoolManager.Instance.PushGameObject(this.name,gameObject);
    }
}