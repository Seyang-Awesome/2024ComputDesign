using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct AudioRandomGroup
{
    public AudioClip[] audioClips;

    public AudioClip GetRandomClip()
    {
        if (audioClips.Length == 0) return null;
        return audioClips[Random.Range(0, audioClips.Length)];
    }
}

