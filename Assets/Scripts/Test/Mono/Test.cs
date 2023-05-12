using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ttt{
    public ttt(){
        MonoManager.Instance.StartCoroutine(t());
    }
    public IEnumerator t(){
        while(true){
            yield return new WaitForSeconds(1f);
            Debug.Log("1");
        }
    }
}
public class Test : MonoBehaviour{
    private void Start() {
        ttt t = new ttt();
        //MonoManager.Instance.AddUpdateListener(t.Update);
    }
}