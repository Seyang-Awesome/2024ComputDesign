using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private BgmContainer bgmContainer;
    [SerializeField] private SoundEffectContainer soundEffectContainer;

    private Dictionary<string, AudioClip> bgms = new();
    private Dictionary<string, AudioClip> soundEffects = new();

    private GameObject audioRoot;
    private AudioSource bgmComponent;
    private AudioSource seComponent;
    // private List<AudioSource> soundEffectComponents = new();

    public float BgmVolume => bgmComponent.volume;
    public float SeVolume => seComponent.volume;
    private void Start()
    {
        bgmContainer.bgms.ForEach(bgm => bgms.Add(bgm.name, bgm));
        soundEffectContainer.soundEffects.ForEach(soundEffect => 
            soundEffects.Add(soundEffect.name, soundEffect));

        audioRoot = Instantiate(new GameObject());
        DontDestroyOnLoad(audioRoot);
        audioRoot.name = "AudioRoot";
        
        bgmComponent = audioRoot.AddComponent<AudioSource>();
        bgmComponent.loop = true;
        seComponent = audioRoot.AddComponent<AudioSource>();
    }

    public void PlayBgm(string bgmName)
    {
        if (!bgms.ContainsKey(bgmName)) return;
        PlayBgm(bgms[bgmName]);
    }

    public void PlayBgm(AudioClip clip)
    {
        if (clip == null) return;
        bgmComponent.clip = clip;
        bgmComponent.Play();
    }

    public void StopBgm()
    {
        bgmComponent.Stop();
    }

    public void PlaySe(string seName)
    {
        if (!soundEffects.ContainsKey(seName)) return;
        PlaySe(soundEffects[seName]);
    }

    public void PlaySe(AudioClip clip)
    {
        if (clip == null) return;
        seComponent.PlayOneShot(clip);
    }

    public void SetVolume(float bgmVolume,float seVolume)
    {
        bgmComponent.volume = bgmVolume;
        seComponent.volume = seVolume;
    }
    
    
}
