using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveBlock : MonoBehaviour
{
    [SerializeField] private Ease ease = Ease.OutQuad;
    [SerializeField] private Vector3 offset;

    private Vector3 origin;
    private Vector3 target;
    private void Start()
    {
        origin = transform.position;
        target = origin + offset;
    }

    public Tween MoveToOrigin()
    {
        return transform.DOMove(origin, Consts.BlockMoveDuration).SetEase(ease);
    }
    
    
    
    
    
    
    public Tween MoveToTarget()
    {
        return transform.DOMove(target, Consts.BlockMoveDuration).SetEase(ease);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + offset);
        Gizmos.DrawWireSphere(transform.position + offset,0.3f);
    }
}

