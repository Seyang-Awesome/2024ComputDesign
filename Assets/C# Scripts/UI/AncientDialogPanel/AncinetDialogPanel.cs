using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AncinetDialogPanel : MonoSingleton<AncinetDialogPanel>
{
    protected override bool IsDontDestroyOnLoad => false;

    [SerializeField] private Graphic[] graphics;
    [SerializeField] private TextMeshProUGUI contentText;
    [SerializeField] private float fadeInOutDuration;
    
    private Color[] colors;

    
    private void Start()
    {
        colors = new Color[graphics.Length];
        for (int i = 0; i < graphics.Length; i++)
        {
            colors[i] = graphics[i].color;
            graphics[i].color = colors[i].GetTransparent();
        }
    }

    public void Show(string content)
    {
        FirstPersonController.Instance.IsCanControl = false;
        FirstPersonController.Instance.SetMouseCursor(CursorLockMode.None);
        
        contentText.text = content;
        for (int i = 0; i < graphics.Length; i++)
        {
            Graphic graphic = graphics[i];
            Color color = colors[i];
            // graphic.color = color.GetTransparent();
            graphic.DOColor(color, fadeInOutDuration);
        }
    }

    public void Hide()
    {
        FirstPersonController.Instance.IsCanControl = true;
        FirstPersonController.Instance.SetMouseCursor(CursorLockMode.Locked);
        
        for (int i = 0; i < graphics.Length; i++)
        {
            Graphic graphic = graphics[i];
            Color color = colors[i];
            graphic.DOColor(color.GetTransparent(), fadeInOutDuration);
        }
    }
}

