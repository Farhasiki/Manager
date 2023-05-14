using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Other : MonoBehaviour{
    private void Awake() {
        EventCenter.Instance.AddEventListener<Monster>(EventName.MonsterDead,OnMonsterDead);
    }
    public void OnMonsterDead(Monster info){
        Debug.Log("其他事件");
    }
}