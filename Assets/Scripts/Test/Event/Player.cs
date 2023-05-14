using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Player : MonoBehaviour{
    private void Awake() {
        EventCenter.Instance.AddEventListener<Monster>(EventName.MonsterDead,OnMonsterDead);
    }
    public void OnMonsterDead(Monster info){
        Debug.Log("玩家杀敌数加1");
    }
}