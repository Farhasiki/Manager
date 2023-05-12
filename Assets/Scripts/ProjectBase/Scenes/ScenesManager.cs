using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : BaseManager<ScenesManager>{
    public void LoadScene(string sceneName,Action action){
        SceneManager.LoadScene(sceneName);
        action?.Invoke();
    }

    public void LoadSceneAsync(string sceneName, Action action){
        MonoManager.Instance.StartCoroutine(ReallyLoadSceneAsync(sceneName,action));
    }

    private IEnumerator ReallyLoadSceneAsync(string sceneName, Action action){
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        while(!asyncOperation.isDone){
            EventCenter.Instance.EventTrigger(EventName.LoadScene,asyncOperation.progress);
            yield return asyncOperation.progress;
        }
        action?.Invoke();
    }
}
