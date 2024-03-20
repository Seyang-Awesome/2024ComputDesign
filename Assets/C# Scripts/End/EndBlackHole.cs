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
        transform.localScale = Vector3.zero;

        anim.onUpdateLastScene += () => transform.localScale = Vector3.one * s;
    }
}
