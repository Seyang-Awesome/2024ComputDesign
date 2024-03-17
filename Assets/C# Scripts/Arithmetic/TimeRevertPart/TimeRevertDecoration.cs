using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRevertDecoration : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float rotateSpeed = 30;

    private float speedModifier = 1;

    private Vector3 rotationAxis; // 旋转轴


    private void Start()
    {
        TimeRevertManager.Instance.onTimeScaleChange += (value) => speedModifier = value;
        rotationAxis = Random.onUnitSphere;
    }

    private void Update()
    {
        var dir = Vector3.down;
        dir *= speedModifier;

        transform.position += dir * speed * Time.deltaTime;
        transform.Rotate(rotationAxis, rotateSpeed * speedModifier * Time.deltaTime);

        CheckPosition();
    }

    private bool isTransforming = false;

    //屎山
    private void CheckPosition()
    {
        if (isTransforming)
            return;

        if(transform.position.y < TimeRevertManager.Instance.decorationMinY)
        {
            isTransforming = true;
            var s = transform.localScale.x;
            DOTween.Sequence()
                .Append(transform.DOScale(Vector3.zero * 0.001f, 0.3f).OnComplete(() => transform.position = new Vector3(transform.position.x, TimeRevertManager.Instance.decorationMaxY, transform.position.z)))
                .Append(transform.DOScale(Vector3.one * s, 0.3f))
                .OnComplete(() => isTransforming = false);
        }

        if(transform.position.y > TimeRevertManager.Instance.decorationMaxY)
        {
            isTransforming = true;
            var s = transform.localScale.x;
            DOTween.Sequence()
                .Append(transform.DOScale(Vector3.one * 0.001f, 0.3f).OnComplete(() => transform.position = new Vector3(transform.position.x, TimeRevertManager.Instance.decorationMinY, transform.position.z)))
                .Append(transform.DOScale(Vector3.one * s, 0.3f))
                .OnComplete(() => isTransforming = false);
        }
    }
}
