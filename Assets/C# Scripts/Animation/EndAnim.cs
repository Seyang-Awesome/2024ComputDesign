using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class EndAnimStepInfo
{
    public GameObject stepPrefab;
    public Vector3 offsetBetween;
    public float intervalBetween;
    public float appearTime;
}

public class EndAnim : Gear, IAnimatable
{
    [SerializeField]
    private Vector3 origin;
    [SerializeField]
    private EndAnimStepInfo stepInfo;
    [SerializeField]
    private GameObject letterPrefab;

    protected override void SwitchOn()
    {
        base.SwitchOn();
        AnimateAction();
    }

    public void AnimateAction()
    {
        StartCoroutine(StepAction());
    }

    private IEnumerator StepAction()
    {
        var interval = new WaitForSeconds(stepInfo.intervalBetween);
        var pos = origin;
        for (int i = 0; i < 1000; i++)
        {
            var step = Instantiate(stepInfo.stepPrefab);

            step.transform.position = pos + Vector3.down * 40;
            step.transform.DOMove(pos,stepInfo.appearTime).SetEase(Ease.OutQuad);
            pos += stepInfo.offsetBetween;
            yield return interval;
        }
    }
}
