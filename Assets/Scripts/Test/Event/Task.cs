using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Task : MonoBehaviour{
    private void Awake() {
        EventCenter.Instance.AddEventListener(EventName.MonsterDead,OnMonsterDead);
    }
    public void OnMonsterDead(object sender, EventArgs e){
        Debug.Log((e as Monster.OnMonsterDeadArgs).name);
        Debug.Log("任务完成");
    }
}