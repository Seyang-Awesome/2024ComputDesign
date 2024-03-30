using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBlackHole : MonoBehaviour
{
    [SerializeField]
    private EndAnim anim;

    private void Start()
    {
        var s = transform.localScale.x;
        //transform.localScale = Vector3.zero;

        var mat = GetComponent<Renderer>().material;
        mat.SetFloat("_DistortionP", 10);
        mat.SetFloat("_DiskRadian", 0);
        anim.onUpdateLastScene += () => OnShow(s);
        //OnShow(s);
    }

    private void OnShow(float s)
    {
        //transform.localScale = Vector3.one * s;
        var mat = GetComponent<Renderer>().material;
        mat.SetFloat("_DistortionP", 10);
        mat.SetFloat("_DiskRadian", 0);
        DOTween.To
            (
            () => mat.GetFloat("_DistortionP"),
            x => mat.SetFloat("_DistortionP", x),
            2.5f,
            2f
            ).SetEase(Ease.OutBack);

        DOTween.To
            (
            () => mat.GetFloat("_DiskRadian"),
            x => mat.SetFloat("_DiskRadian", x),
            0.25f,
            2f
            ).SetEase(Ease.OutBack);
    }
}
