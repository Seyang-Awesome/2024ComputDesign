using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIFadeAnimation : MonoBehaviour, ITipable
{
    [SerializeField] private List<Graphic> graphics = new();
    private Dictionary<Graphic, Color> graphicsColors = new();
    private List<Tween> previousExitTweens = new();
    
    private void Start()
    {
        graphics.ForEach(graphic =>
        {
            graphicsColors.Add(graphic, graphic.color);
            graphic.color = Color.clear;
            graphic.gameObject.SetActive(false);
        });
    }
    
    public virtual void OnEnter(Player player)
    {
        if (previousExitTweens.Count != 0)
        {
            previousExitTweens.ForEach(previousExitTween => previousExitTween.onComplete = null);
            previousExitTweens.Clear();
        }
        
        graphics.ForEach(graphic =>
        {
            graphic.gameObject.SetActive(true);
            graphic.color = Color.clear;
            graphic.DOColor(graphicsColors[graphic], Consts.UIAppearDuration);
        });
    }

    public virtual void OnExit(Player player)
    {
        graphics.ForEach(graphic =>
        {
            Tween tween = graphic.DOColor(Color.clear, Consts.UIAppearDuration);
            tween.onComplete += () => graphic.gameObject.SetActive(false);
            previousExitTweens.Add(tween);
        });
    }
}

