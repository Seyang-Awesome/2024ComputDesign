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
    private void Start()
    {
        graphics.ForEach(graphic =>
        {
            graphicsColors.Add(graphic, graphic.color);
            graphic.color = Color.clear;
        });
    }
    
    public virtual void OnEnter(Player player)
    {
        graphics.ForEach(graphic =>
        {
            graphic.DOColor(graphicsColors[graphic], Consts.UIAppearDuration);
        });
    }

    public virtual void OnExit(Player player)
    {
        graphics.ForEach(graphic => graphic.DOColor(Color.clear, Consts.UIAppearDuration));
    }
}

