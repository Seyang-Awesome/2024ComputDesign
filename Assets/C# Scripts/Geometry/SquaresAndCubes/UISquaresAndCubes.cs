using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DG.DemiLib;

public class UISquaresAndCubes : Publisher
{
    [SerializeField] private TextMeshProUGUI lengthText;
    [SerializeField] private TextMeshProUGUI dimensionText;
    [SerializeField] private Button lengthUpButton;
    [SerializeField] private Button lengthDownButton;
    [SerializeField] private Button dimensionUpButton;
    [SerializeField] private Button dimensionDownButton;
    
    private LinkedList<int> lengthList;
    private LinkedList<int> dimensionList;

    private LinkedListNode<int> currentLength;
    private LinkedListNode<int> currentDimension;

    private float lengthFontSize;
    private float dimensionFontSize;
    
    
    
    private void Start()
    {
        lengthList = new(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
        dimensionList = new(new int[] {3, 1, 2});

        currentLength = lengthList.First;
        currentDimension = dimensionList.First;

        lengthUpButton.onClick.AddListener(OnClickLengthUpButton);
        lengthDownButton.onClick.AddListener(OnClickLengthDownButton);
        dimensionUpButton.onClick.AddListener(OnClickDimensionUpButton);
        dimensionDownButton.onClick.AddListener(OnClickDimensionDownButton);

        lengthFontSize = lengthText.fontSize;
        dimensionFontSize = dimensionText.fontSize;
    }

    private void OnClickLengthUpButton()
    {
        currentLength = currentLength.Next ?? lengthList.First;
        ShowLengthText();
        Publish();
    }
    
    private void OnClickLengthDownButton()
    {
        currentLength = currentLength.Previous ?? lengthList.Last;
        ShowLengthText();
        Publish();
    }
    
    private void OnClickDimensionUpButton()
    {
        currentDimension = currentDimension.Next ?? dimensionList.First;
        ShowDimensionText();
        Publish();
    }
    
    private void OnClickDimensionDownButton()
    {
        currentDimension = currentDimension.Previous ?? dimensionList.Last;
        ShowDimensionText();
        Publish();
    }

    private void ShowLengthText()
    {
        lengthText.DOBounceFontSize(lengthFontSize, currentLength.Value.ToString(), Consts.UIChangeDuration);
    }

    private void ShowDimensionText()
    {
        dimensionText.DOBounceFontSize(dimensionFontSize, currentDimension.Value.ToString(), Consts.UIChangeDuration);
    }

    private void Publish()
    {
        SetStateAndInvoke(0, new SquaresAndCubesArgs(currentLength.Value, currentDimension.Value));
    }
    
}

