using System.Security;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour{
    public new string name = "132";
    public class OnMonsterDeadArgs : System.EventArgs{
        public string name;
    }
    private void Start() {
        System.Action  t = null;
        Dead();
    }
    public void Dead(){
        Debug.Log("怪物死了");
        // EventCenter.Instance.EventTrigger(EventName.MonsterDead,this,new OnMonsterDeadArgs{
        //     name = this.name
        // });
        EventCenter.Instance.EventTrigger<Monster>(EventName.MonsterDead,this);
    }
}