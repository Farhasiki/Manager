using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : BaseManager<MusicManager>{
    private const string MUSIC_RESOURCES = "";
    private const string SOUND_RESOURCES = "";
    private AudioSource BGM_Music = null;
    private float BGM_Value = 1f;
    private float soundValue = 1f;
    private GameObject soundGameObject = null;
    private List<AudioSource> soundList = new List<AudioSource>();

    public MusicManager(){
        MonoManager.Instance.AddUpdateListener(Update);
    }

    private void Update() {
        for(int i = soundList.Count - 1; i >= 0; --i){
            if(!soundList[i].isPlaying){
                GameObject.Destroy(soundList[i]);
                soundList.RemoveAt(i);
            }
        }
    }

    public void PlayBackGroundMusic(string name){
        if(BGM_Music == null){
            GameObject gameObject = new GameObject("BGM_Music");
            BGM_Music = gameObject.AddComponent<AudioSource>();
        }

        // 异步加载音乐
        ResManager.Instance.LoadAsync<AudioClip>(MUSIC_RESOURCES + name,(clip)=>{
            BGM_Music.clip = clip;
            BGM_Music.loop = true;
            BGM_Music.volume = BGM_Value;
            BGM_Music.Play();
        });
    }

    /// <summary>
    /// 暂停播放背景音乐
    /// </summary>
    public void PauseBGMusic(){
        BGM_Music?.Pause();
    }
    /// <summary>
    /// 停止播放背景音乐
    /// </summary>
    public void StopBKMusic(){
        BGM_Music?.Stop();
    }

    /// <summary>
    /// 改变音乐大小
    /// </summary>
    public void ChangeBGMValue(float value){
        BGM_Value = value;
        if(BGM_Music == null)return ;
        BGM_Music.volume = value;
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    public void PlaySound(string name,bool isLoop,System.Action<AudioSource> callbcak = null){
        if(soundGameObject == null){
            soundGameObject = new GameObject("Sound");
        }
        /// <summary>
        /// 资源加载结束后播放
        /// </summary>
        ResManager.Instance.LoadAsync<AudioClip>(SOUND_RESOURCES + name,(clip)=>{
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.volume = soundValue;
            audioSource.loop = isLoop;
            audioSource.Play();
            soundList.Add(audioSource);
            if(callbcak != null)callbcak(audioSource);
        });
    }
    /// <summary>
    /// 停止播放
    /// </summary>
    public void StopSound(AudioSource source){
        if(soundList.Contains(source)){
            soundList.Remove(source);
            source.Stop();
            GameObject.Destroy(source);
        }
    }

    /// <summary>
    /// 改变音效声音
    /// </summary>
    public void ChangeSoundValue(float value){
        soundValue = value;
        foreach(var sound in soundList){
            sound.volume = value;
        }
    }
}
