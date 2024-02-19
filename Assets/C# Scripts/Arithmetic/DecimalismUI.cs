using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DG.DemiLib;

public class DecimalismUI : Publisher
{
    [SerializeField] private TextMeshProUGUI lengthText;
    [SerializeField] private Button lengthUpButton;
    [SerializeField] private Button lengthDownButton;
    public List<MoveBlock> blocks = new();
    private List<int> lengthList;//双向链表
    private int currentLength;


    private float lengthFontSize;
    private float dimensionFontSize;
    
    private void Start()
    {
        lengthList = new(new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9,10 });
        currentLength = 0;

        lengthUpButton.onClick.AddListener(OnClickLengthUpButton);
        lengthDownButton.onClick.AddListener(OnClickLengthDownButton);

        lengthFontSize = lengthText.fontSize;
        
    }

    private void OnClickLengthUpButton()
    {
        Debug.Log("按上键");
        blocks[currentLength].MoveToOrigin();
        if (currentLength == 0)
        {
            currentLength = 10;
        }
        else
        {
            currentLength--;
        }

        ShowLengthText();
        
    }
    
    private void OnClickLengthDownButton()
    {
        Debug.Log("按下键");
        if (currentLength == 10)
        {
            currentLength = 0;
        }
        else
        {
            currentLength++;
        }
        blocks[currentLength].MoveToTarget();
        ShowLengthText();
    }
    

    private void ShowLengthText()
    {
        lengthText.DOBounceFontSize(lengthFontSize, currentLength.ToString(), Consts.UIChangeDuration);
    }
    
    
}

