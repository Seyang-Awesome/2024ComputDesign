using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInteractable : MonoBehaviour, IInteractable, ITipable
{
    [SerializeField] private List<Graphic> graphics;
    
    [SerializeField] private UISwitch uiSwitch;

    #region Unity

    private void Start()
    {
        graphics.ForEach(graphic => graphic.gameObject.SetActive(false));
    }
    
    #endregion
    
    public virtual void OnEnter(Player player)
    {
        graphics.ForEach(graphic =>
        {
            graphic.gameObject.SetActive(true);
            graphic.color = Color.clear;
            graphic.DOColor(Color.white, Consts.UIAppearDuration);
        });
    }

    public virtual void OnExit(Player player)
    {
        graphics.ForEach(graphic =>
        {
            graphic.DOColor(Color.clear,Consts.UIAppearDuration)
                .onComplete += () => graphic.gameObject.SetActive(false);
        });
    }

    public virtual void OnInteract(Player player)
    {
        uiSwitch.SwitchState();
    }
}

