using System;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Triangle : MonoBehaviour
{
    [SerializeField] private UIPythagorean uiPythagorean;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material material;
    [SerializeField] private float size = 1f;

    [SerializeField] private Color correctColor;
    [SerializeField] private Color wrongColor;
    
    private void Start()
    {
        uiPythagorean.onStateChanged += UpdateShape;
        uiPythagorean.onRequestCheck += TryToCheck;

        //Instantiate一个material出来，后面要改颜色的
        meshRenderer.material = Instantiate(material);
    }

    private void OnDestroy()
    {
        uiPythagorean.onStateChanged -= UpdateShape;
        uiPythagorean.onRequestCheck -= TryToCheck;
    }

    private void UpdateShape(int length, int width)
    {
        transform.DOScale(new Vector3(1, length * size, width * size), Consts.ObjectsChangeDuration);
    }

    private bool TryToCheck()
    {
        bool result = ((uiPythagorean.CurrentLength == 3 && uiPythagorean.CurrentWidth == 4) ||
                       (uiPythagorean.CurrentLength == 4 && uiPythagorean.CurrentWidth == 3));
        Color showColor = result ? correctColor : wrongColor;
        float showAngle = Mathf.Atan((float)uiPythagorean.CurrentLength / (float)uiPythagorean.CurrentWidth) / Mathf.Deg2Rad;

        if (result)
            DOTween.Sequence()
                .Append(transform.DOLocalRotate(new Vector3(showAngle, 0, 0), Consts.ObjectsChangeDuration))
                .Append(meshRenderer.material.DOColor(showColor,"_TintColor", Consts.ObjectsFlashDuration).SetEase(Ease.Flash,4));
        else
            DOTween.Sequence()
                .Append(transform.DOLocalRotate(new Vector3(showAngle, 0, 0), Consts.ObjectsChangeDuration))
                .Append(meshRenderer.material.DOColor(showColor,"_TintColor", Consts.ObjectsFlashDuration).SetEase(Ease.Flash,4))
                .Append(transform.DOLocalRotate(new Vector3(0, 0, 0), Consts.ObjectsChangeDuration));

        return result;
    }
}

