using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ApexDialogBoxGroupPusher : MonoBehaviour
{
    public ApexDialogBoxGroup group;
    public float delay;
    public void PushGroup()
    {
        DOTween.Sequence()
            .AppendInterval(delay)
            .AppendCallback(() => ApexDialogBoxPanel.Instance.PushDialogContent(group));
    }
}

