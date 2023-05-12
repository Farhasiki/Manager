using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Player : MonoBehaviour{
    private void Awake() {
        EventCenter.Instance.AddEventListener(EventName.MonsterDead,OnMonsterDead);
    }
    public void OnMonsterDead(object sender, EventArgs e){
        Debug.Log("玩家杀敌数加1");
    }
}