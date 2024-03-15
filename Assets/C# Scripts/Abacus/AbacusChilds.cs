using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbacusChilds : MonoBehaviour
{
    [HideInInspector]
    public bool isHighDigit = false;

    [SerializeField]
    private Vector3 offset;

    private AbacusLines parent;
    private Vector3 taretPos;
    private Vector3 originPos;

    private bool isUp;

    private void Start()
    {
        parent = GetComponentInParent<AbacusLines>();
        taretPos = transform.position + offset;
        originPos = transform.position;
    }

    private void OnMouseDown()
    {
        if (isUp)
            parent.MoveDown(isHighDigit);
        else
            parent.MoveUp(isHighDigit);
    }

    public void MoveUp()
    {
        transform.DOMove(taretPos, 0.3f).SetEase(Ease.OutQuad);
        isUp = true;
    }

    public void MoveDown() 
    {
        transform.DOMove(originPos, 0.3f).SetEase(Ease.OutQuad);
        isUp = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + offset, 1f);
        Gizmos.DrawLine(transform.position, transform.position + offset);
    }
}
