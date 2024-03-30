using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ApexDialogboxGroupTriggerPusher : MonoBehaviour, ITipable
{
    public ApexDialogBoxGroup group;
    public float delay;
    private bool isActive = true;
    public void OnEnter(Player player)
    {
        if (!isActive) return;
        
        DOTween.Sequence()
            .AppendInterval(delay)
            .AppendCallback(() => ApexDialogBoxPanel.Instance.PushDialogContent(group));
        isActive = false;
    }

    public void OnExit(Player player)
    {
        
    }
}

