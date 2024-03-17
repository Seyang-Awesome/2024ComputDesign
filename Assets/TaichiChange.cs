using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaichiChange : MonoBehaviour
{
    private Image image;
    [SerializeField]private Sprite SpriteClose;
    [SerializeField]private Sprite spriteOpen;
    [SerializeField]private bool isOn;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Change()
    {
                
        isOn = !isOn;
        if (isOn)
        {
            image.sprite = spriteOpen;
        }
        else
        {
            image.sprite = SpriteClose;
        }
    }
}
