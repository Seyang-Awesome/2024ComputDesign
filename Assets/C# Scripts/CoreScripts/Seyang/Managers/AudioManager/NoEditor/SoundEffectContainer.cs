using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectContainer : ScriptableObject
{
    public List<AudioClip> soundEffects = new();
    
    public void AddSoundEffect(AudioClip audioClip)
    {
        foreach (var item in soundEffects)
        {
            if (item == null)
            {
                soundEffects.Remove(item);
                continue;
            }
            if (item.name == audioClip.name) return;

        }
        soundEffects.Add(audioClip);
    }
}
