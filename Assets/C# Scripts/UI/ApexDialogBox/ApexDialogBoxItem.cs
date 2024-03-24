using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ApexDialogBoxItem : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI contentText;
    [SerializeField] private float fadeInOutDuration;

    private Color backgroundColor;
    private Color contentTextColor;
    
    public void Init()
    {
        backgroundColor = background.color;
        contentTextColor = contentText.color;
        background.color = backgroundColor.GetTransparent();
        contentText.color = contentTextColor.GetTransparent();
    }
    
    public void Show(ApexDialogBoxGroup group,Vector2 startPos, Vector2 endPos,Action<ApexDialogBoxItem> callback)
    {
        transform.position = startPos;
        contentText.SetText(group.content);
        background.color = backgroundColor.GetTransparent();
        contentText.color = contentTextColor.GetTransparent();
        Vector2 centerPos = (startPos + endPos) / 2;
            
        Sequence sequence = DOTween.Sequence();
        sequence
            .Append(background.DOColor(backgroundColor, fadeInOutDuration))
            .Join(contentText.DOColor(contentTextColor, fadeInOutDuration))
            .Join(transform.DOMove(centerPos, fadeInOutDuration))
            .AppendCallback(() => AudioManager.Instance.PlaySe(group.clip))
            .AppendInterval(group.Duration).OnComplete(() => callback?.Invoke(this));
        sequence
            .Append(background.DOColor(backgroundColor.GetTransparent(), fadeInOutDuration))
            .Join(contentText.DOColor(contentTextColor.GetTransparent(), fadeInOutDuration))
            .Join(transform.DOMove(endPos, fadeInOutDuration));
    }
}

