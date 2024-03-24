using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TaichiMoveBlock : MonoBehaviour
{
    [SerializeField] private Ease ease = Ease.OutQuad;
    [SerializeField] private Vector3[] offset = new Vector3[3];
    [SerializeField] private bool[] OnCoverted = new bool[3];
    private Vector3 origin;
    private Vector3 target;

    private void Start()
    {
        origin = transform.position;
        //      target = origin + offset;11
    }

    public void Convert(int index)
    {
        if (OnCoverted[index])
        {
            MoveToOrigin(index);
            OnCoverted[index] = false;
        }

        else
        {
            MoveToTarget(index);
            OnCoverted[index] = true;
        }

    }

    public Tween MoveToOrigin(int index)
    {
        return transform.DOMove(transform.position - offset[index], Consts.BlockMoveDuration).SetEase(ease);
    }

    public Tween MoveToTarget(int index)
    {
        return transform.DOMove(transform.position + offset[index], Consts.BlockMoveDuration).SetEase(ease);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + offset[0]);
        Gizmos.DrawLine(transform.position, transform.position + offset[1]);
        Gizmos.DrawLine(transform.position, transform.position + offset[2]);
        Gizmos.DrawWireSphere(transform.position + offset[0], 0.3f);
        Gizmos.DrawWireSphere(transform.position + offset[1], 0.3f);
        Gizmos.DrawWireSphere(transform.position + offset[2], 0.3f);
    }
}
