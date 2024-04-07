using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBlackHole : MonoBehaviour
{
    [SerializeField]
    private EndAnim anim;
    [SerializeField]
    private GameObject blackHole;

    private void Start()
    {
        blackHole.SetActive(false);
        anim.onUpdateLastScene += () => blackHole.SetActive(true);
        //OnShow(s);
    }

    private void Update()
    {
        blackHole.transform.Rotate(Vector3.up, Time.deltaTime * 10, Space.Self);
    }

}
