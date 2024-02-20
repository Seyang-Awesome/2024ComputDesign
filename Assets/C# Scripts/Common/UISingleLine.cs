using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class UISingleLine
{
    [SerializeField] private RectTransform root;
    [SerializeField] private LineRenderer line;
    [SerializeField] private TextMeshProUGUI text;
    private float fontSize;

    public Vector2 Origin
    {
        get => root.localPosition;
        private set => root.localPosition = value;
    }

    public Vector2 End => new Vector2(
        Mathf.Cos(Angle * Mathf.Deg2Rad), 
        Mathf.Sin(Angle * Mathf.Deg2Rad)
        ) * Length - Origin;

    public float Length
    {
        get => line.GetPosition(1).x;
        private set
        {
            line.SetPosition(1, new Vector3(value, 0));
            root.sizeDelta = new Vector2(value, root.sizeDelta.y);
        }
    }

    private float Angle => root.localRotation.eulerAngles.z;

    public UISingleLine(RectTransform root, LineRenderer line, TextMeshProUGUI text)
    {
        this.root = root;
        this.line = line;
        this.text = text;
        Init();
    }

    public void Init()
    {
        if (text == null) return;
        fontSize = text.fontSize;
    }

    public void SetLine(Vector2 origin, Vector2 direction, float duration)
    {
        float length = direction.magnitude;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        root.DOLocalMove(origin, duration);
        root.DOLocalRotate(new Vector3(0, 0, angle), duration);
        DOTween.To(
            () => Length,
            x => Length = x,
            length,
            duration);
        if(text != null)
            text.rectTransform.DOLocalRotate(-new Vector3(0, 0, angle), duration);
    }

    public void SetLineByPoints(Vector2 point0, Vector2 point1, float duration)
    {
        SetLine(point0, point1 - point0, duration);
    }

    public void SetText(string endValue, float duration)
    {
        if(text != null)
            text.DOBounceFontSize(fontSize, endValue, duration);
    }
}

