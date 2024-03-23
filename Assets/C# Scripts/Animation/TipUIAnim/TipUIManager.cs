using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct TipUIInfo
{
    public string title;
    [Multiline(3)]
    public string contain;
}


public class TipUIManager : MonoSingleton<TipUIManager>
{
    [SerializeField]
    private TipUIInfo[] tipUIInfo;

    [SerializeField]
    private float changeDuration = 1.5f;

    [SerializeField]
    private float holdDuration = 2f;

    [SerializeField]
    private Image outLine;

    [SerializeField]
    private TextMeshProUGUI title;

    [SerializeField]
    private TextMeshProUGUI contain;

    private void Start()
    {
        UpdateTip(0);
        title.color = Color.white.GetTransparent();
        contain.color = Color.white.GetTransparent();
    }

    public void UpdateTip(int index)
    {
        outLine.fillAmount = 0;
        outLine.color = Color.white;
        title.text = tipUIInfo[index].title;
        contain.text = tipUIInfo[index].contain;

        DOTween.Sequence()
            .Append(outLine.DOFillAmount(1, changeDuration / 2).SetEase(Ease.OutQuad))
            .Join(title.DOColor(Color.white, changeDuration))
            .Join(contain.DOColor(Color.white, changeDuration))
            .AppendInterval(holdDuration)
            .Append(outLine.DOColor(Color.white.GetTransparent(), changeDuration))
            .Join(title.DOColor(Color.white.GetTransparent(), changeDuration))
            .Join(contain.DOColor(Color.white.GetTransparent(), changeDuration));
    }
}
