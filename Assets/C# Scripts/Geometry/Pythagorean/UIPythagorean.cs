using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPythagorean : MonoBehaviour
{
    [SerializeField] private float size = 1f;
    
    [SerializeField] private RectTransform lengthRoot;
    [SerializeField] private TextMeshProUGUI lengthText;
    [SerializeField] private Button lengthButton;
    private LinkedList<int> lengthList;
    private LinkedListNode<int> currentLengthNode;
    private float lengthFontSize;
    public int CurrentLength => currentLengthNode.Value;
    
    [SerializeField] private RectTransform widthRoot;
    [SerializeField] private TextMeshProUGUI widthText;
    [SerializeField] private Button widthButton;
    private LinkedList<int> widthList;
    private LinkedListNode<int> currentWidthNode;
    private float widthFontSize;
    public int CurrentWidth => currentWidthNode.Value;

    [SerializeField] private Button checkButton;
    
    public event Action<int, int> onStateChanged;
    public event Func<bool> onRequestCheck;

    private void Start()
    {
        lengthList = new(new int[] { 1, 2, 3, 4, 5 });
        currentLengthNode = lengthList.First;
        lengthFontSize = lengthText.fontSize;
        lengthButton.onClick.AddListener(OnClickLengthButton);
        lengthRoot.sizeDelta = new Vector2(CurrentLength * size, lengthRoot.sizeDelta.y);
        lengthText.text = CurrentLength.ToString();
        
        widthList = new(new int[] { 1, 2, 3, 4, 5 });
        currentWidthNode = widthList.First;
        widthFontSize = widthText.fontSize;
        widthButton.onClick.AddListener(OnClickWidthButton);
        widthRoot.sizeDelta = new Vector2(CurrentWidth * size, widthRoot.sizeDelta.y);
        widthText.text = CurrentWidth.ToString();
        
        checkButton.onClick.AddListener(() =>
        {
            //如果Check正确，则不再接受点击事件
            bool? result = onRequestCheck?.Invoke();
            if (result.HasValue && result.Value)
            {
                lengthButton.image.raycastTarget = false;
                widthButton.image.raycastTarget = false;
                checkButton.image.raycastTarget = false;
            }
        });
    }

    private void OnClickLengthButton()
    {
        currentLengthNode = currentLengthNode.Next ?? lengthList.First;
        lengthRoot.DOSizeDelta(new Vector2(CurrentLength * size, lengthRoot.sizeDelta.y), Consts.UIChangeDuration);
        lengthText.DOBounceFontSize(lengthFontSize, CurrentLength.ToString(), Consts.UIChangeDuration);
        onStateChanged?.Invoke(CurrentLength,CurrentWidth);
    }
    
    private void OnClickWidthButton()
    {
        currentWidthNode = currentWidthNode.Next ?? widthList.First;
        widthRoot.DOSizeDelta(new Vector2(CurrentWidth * size, widthRoot.sizeDelta.y), Consts.UIChangeDuration);
        widthText.DOBounceFontSize(widthFontSize, CurrentWidth.ToString(), Consts.UIChangeDuration);
        onStateChanged?.Invoke(CurrentLength,CurrentWidth);
    }
}

