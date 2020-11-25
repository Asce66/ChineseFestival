using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMnr : MonoBehaviour
{
    private static AudioMnr _ins;
    public static AudioMnr _Ins
    {
        get
        {
            return _ins;
        }
    }
    private AudioClip btnClip,paging;
    private AudioSource bgAudio, otherAudio;
    bool isMute = false;
    private void Awake()
    {
        _ins = this;
        DontDestroyOnLoad(gameObject);
        InitAudioSource();
        btnClip = GetAudioClip("Button");
        SceneBGChange(1);
        paging = GetAudioClip("Paging");
    }
    void InitAudioSource()
    {
        bgAudio = gameObject.AddComponent<AudioSource>();
        bgAudio.loop = true;
        bgAudio.volume = 0.17f;

        otherAudio = gameObject.AddComponent<AudioSource>();
        otherAudio.volume = 0.2f;
    }

    AudioClip GetAudioClip(string clipName)
    {
        string path = "AudioClips/"+clipName;
        AudioClip clip = Resources.Load<AudioClip>(path);
        if(clip==null){
            Debug.LogError("获得Audio Clip的路径出错: " + path);
        }
        return clip;
    }

    public void PlayBtnClickAudio()
    {
        if (isMute==false)
            otherAudio.PlayOneShot(btnClip);
    }

    public void PlayPaging()
    {
        if (isMute == false)
            otherAudio.PlayOneShot(paging);
    }

    public bool AudioSwitch()
    {
        isMute = !isMute;
        if (isMute && bgAudio.isPlaying)
            bgAudio.Pause();
        else if (isMute == false && bgAudio.isPlaying == false)
            bgAudio.Play();
        return isMute;
    }

    public void SceneBGChange(int sceneID)
    {
        if (isMute||sceneID==2)
            return;
        AudioClip audioClip = GetAudioClip("BG" + sceneID);
        bgAudio.clip = audioClip;
        if (bgAudio.isPlaying == false)
            bgAudio.Play();
    }

    public void PauseBGAudio()
    {
        bgAudio.Pause();
    }

}
