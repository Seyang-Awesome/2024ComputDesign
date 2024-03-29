using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class CheckKeyToChangeScene : MonoBehaviour, ITipable
{
    public KeyCode keyCode;
    public string sceneName;
    public UnityEvent onChangeScene;
    public float deley;
    private bool isCanChangeScene = false;

    private void Update()
    {
        Debug.Log(isCanChangeScene);
        if (isCanChangeScene && Input.GetKeyDown(keyCode))
        {
            onChangeScene?.Invoke();
            DOTween.Sequence()
                .AppendInterval(deley)
                .AppendCallback(() =>
                {
                    Cover.Instance.SetBackgroundColor(Color.black);
                    Cover.Instance.ChangeScene(sceneName);
                });
        }
    }

    public void OnEnter(Player player)
    {
        isCanChangeScene = true;
    }

    public void OnExit(Player player)
    {
        isCanChangeScene = false;
    }
}

