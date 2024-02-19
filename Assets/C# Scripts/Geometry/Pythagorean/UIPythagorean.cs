using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPythagorean : MonoBehaviour
{
    private const float Size = 1f;
    
    [SerializeField] private RectTransform lengthRoot;
    [SerializeField] private TextMeshProUGUI lengthText;
    [SerializeField] private Button lengthButton;
    private LinkedList<int> lengthList;
    private LinkedListNode<int> currentLengthNode;
    private float lengthFontSize;
    private int CurrentLength => currentLengthNode.Value;

    private void Start()
    {
        lengthList = new(new int[] { 1, 2, 3, 4, 5 });
        currentLengthNode = lengthList.First;
        lengthButton.onClick.AddListener(OnClickLengthButton);
        lengthFontSize = lengthText.fontSize;
    }

    private void OnClickLengthButton()
    {
        currentLengthNode = currentLengthNode.Next ?? lengthList.First;
        lengthRoot.DOSizeDelta(new Vector2(CurrentLength * Size, lengthRoot.sizeDelta.y), Consts.UIChangeDuration);
        lengthText.DOBounceFontSize(lengthFontSize, CurrentLength.ToString(), Consts.UIChangeDuration);
    }
}

