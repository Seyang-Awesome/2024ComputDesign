using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class ChangeSceneTrigger : MonoBehaviour, ITipable
{
    public string sceneName;
    public UnityEvent onChangeScene;
    public float deley;
    private bool isActive = true;

    public void OnEnter(Player player)
    {
        if (!isActive) return;
        
        onChangeScene?.Invoke();
        DOTween.Sequence()
            .AppendInterval(deley)
            .AppendCallback(() =>
            {
                Cover.Instance.SetBackgroundColor(Color.black);
                Cover.Instance.ChangeScene(sceneName);
            });
        isActive = false;
    }

    public void OnExit(Player player)
    {
        
    }
}

