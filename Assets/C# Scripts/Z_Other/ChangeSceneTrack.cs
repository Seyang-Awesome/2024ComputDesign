using System;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneTrack : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        Cover.Instance.ChangeScene(sceneName);
    }
}

