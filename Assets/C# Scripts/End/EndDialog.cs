using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class EndDialog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private ApexDialogBoxGroup[] groups;
    [SerializeField] private float fadeInOutDuration;
    [SerializeField] private string onCompleteSceneName;
    private void Start()
    {
        Color textStartColor = text.color;
        text.color = text.color.GetTransparent();
        
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(1f);
        foreach (var group in groups)
        {
            sequence
                .AppendCallback(() => text.text = group.content)
                .AppendCallback(() => AudioManager.Instance.PlaySe(group.clip))
                .Append(text.DOColor(textStartColor, fadeInOutDuration))
                .AppendInterval(group.Duration)
                .Append(text.DOColor(textStartColor.GetTransparent(), fadeInOutDuration))
                .AppendInterval(fadeInOutDuration);
        }

        sequence.onComplete += () => Cover.Instance.ChangeScene(onCompleteSceneName);
    }
}

