using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        img.DOColor(Color.white.GetTransparent() + new Color(0, 0, 0, 0.1f), 0.3f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        img.DOColor(Color.white.GetTransparent(), 0.3f);
    }

    private Image img;

    private void Start()
    {
        img = transform.GetChild(0).GetComponent<Image>();
    }
}
