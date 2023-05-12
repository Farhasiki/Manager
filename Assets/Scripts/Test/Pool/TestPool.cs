using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPool : MonoBehaviour{
    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            PoolManager.Instance.GetGameObjectAsync("Prefabs/Cube",null);
        }
        if(Input.GetMouseButtonDown(1)){
            //PoolManager.Instance.GetGameObject("Prefabs/Sphere");
            ResManager.Instance.LoadAsync<GameObject>("Prefabs/Sphere",(obj)=>{
                obj.transform.localScale = 2 * Vector3.one;
            });
        }
    }
}
