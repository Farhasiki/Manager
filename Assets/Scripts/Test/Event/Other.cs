using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Other : MonoBehaviour{
    private void Awake() {
        EventCenter.Instance.AddEventListener(EventCenter.EventName.MonsterDead,OnMonsterDead);
    }
    public void OnMonsterDead(object sender, EventArgs e){
        Debug.Log("其他事件");
    }
}