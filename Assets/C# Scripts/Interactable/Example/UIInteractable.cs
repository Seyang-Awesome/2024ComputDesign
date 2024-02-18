using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UISwitch))]
public class UIInteractable : MonoBehaviour, IInteractable, ITipable
{
     [SerializeField] private Image panelImage;
     [SerializeField] private float duration;
     private GameObject panelRoot => panelImage.gameObject;
    
    private UISwitch uiSwitch;

    #region Unity

    private void Start()
    {
        uiSwitch = GetComponent<UISwitch>();
        panelRoot.SetActive(false);
    }
    
    #endregion
    
    public void OnEnter(Player player)
    {
        Debug.Log("OnEnter");
        
        panelRoot.SetActive(true);
        panelImage.color = Color.clear;
        panelImage.DOColor(Color.white, duration);
    }

    public void OnExit(Player player)
    {
        Debug.Log("OnExit");

        panelImage.DOColor(Color.clear,duration)
            .onComplete += () => panelRoot.SetActive(false);
    }

    public void OnInteract(Player player)
    {
        Debug.Log("OnInteract");
        uiSwitch.SwitchState();
    }
}
