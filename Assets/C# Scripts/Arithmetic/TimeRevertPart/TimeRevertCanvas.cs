using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRevertCanvas : MonoBehaviour
{
    [SerializeField]
    private float radius = 3f;
    private void Start()
    {
        transform.localScale = Vector3.zero ;
        TimeRevertManager.Instance.onTrigger += OnTrigger;
    }

    private void OnTrigger()
    {
        DOTween.Sequence()
            .AppendInterval(2f)
            .Append(transform.DOScale(Vector3.one, 0.5f));
    }

    private void OnMouseDown()
    {
        if ((transform.position - Player.Instance.transform.position).magnitude < radius)
        {
            DOTween.Sequence()
                .AppendInterval(1.5f)
                .Append(transform.DOMove(transform.position + Vector3.up * 5, 0.5f).SetEase(Ease.InQuad));
         
            TimeRevertManager.Instance.RevertTime();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
