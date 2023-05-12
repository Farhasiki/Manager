using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPool : MonoBehaviour{
    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            //PoolManager.Instance.GetGameObject("Prefabs/Cube");
            GameObject obj = ResManager.Instance.Load<GameObject>("Prefabs/Sphere");
            obj.transform.localScale = 2 * Vector3.one;
        }
        if(Input.GetMouseButtonDown(1)){
            //PoolManager.Instance.GetGameObject("Prefabs/Sphere");
            ResManager.Instance.LoadAsync<GameObject>("Prefabs/Sphere",(obj)=>{
                obj.transform.localScale = 2 * Vector3.one;
            });
        }
    }
}
