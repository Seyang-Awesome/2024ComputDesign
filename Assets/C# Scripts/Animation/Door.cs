using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Gear
{

    [SerializeField]
    private Vector3 doorDir;


    protected override void SwitchOn()
    {
        base.SwitchOn();

        transform.DOMove(transform.position + doorDir,1.5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + doorDir, 0.1f);
        Gizmos.DrawLine(transform.position,transform.position + doorDir);
    }
}
