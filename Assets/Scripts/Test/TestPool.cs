using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPool : MonoBehaviour{
    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            PoolManager.Instance.GetGameObject("Prefabs/Cube");
        }
        if(Input.GetMouseButtonDown(1)){
            PoolManager.Instance.GetGameObject("Prefabs/Sphere");
        }
    }
}
