using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UITriangle : MonoBehaviour
{
    [SerializeField] private float size = 1f;
    [SerializeField] private UIPythagorean uiPythagorean;
    [SerializeField] private Transform triangleRoot;

    [SerializeField] private UISingleLine lengthLine;
    [SerializeField] private UISingleLine widthLine;
    [SerializeField] private UISingleLine edgeLine;
    
    private void Start()
    {
        uiPythagorean.onStateChanged += UpdateTriangle;
        
        lengthLine.Init();
        widthLine.Init();
        edgeLine.Init();
    }

    private void OnDestroy()
    {
        uiPythagorean.onStateChanged -= UpdateTriangle;
    }
    
    private void UpdateTriangle(int length, int width)
    {
        //保持三角形的几何中心位置不变
        triangleRoot.DOLocalMove(
            -new Vector3(width * size, length * size) * 0.5f, 
            Consts.UIChangeDuration);
        
        if (!(length * size).IsFloatsEqual(lengthLine.Length))
        {
            lengthLine.SetLine(
                Vector2.zero,
                new Vector2(0, length * size),
                Consts.UIChangeDuration);
            lengthLine.SetText(length.ToString(), Consts.UIChangeDuration);
        }
        
        if (!(width * size).IsFloatsEqual(widthLine.Length))
        {
            widthLine.SetLine(
                Vector2.zero,
                new Vector2(width * size, 0),
                Consts.UIChangeDuration);
            widthLine.SetText(width.ToString(), Consts.UIChangeDuration);
        }
        
        edgeLine.SetLineByPoints(
            new Vector2(0, length * size), 
            new Vector2(width * size, 0), 
            Consts.UIChangeDuration);
        edgeLine.SetText(
            Vector2.Distance(new Vector2(0,width), new Vector2(length, 0)).ToString("0.000"),
            Consts.UIChangeDuration);
    }
}

