using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbacusFrame : MonoBehaviour
{
    private void Start()
    {
        GetComponentInParent<Abacus>().onFinishGame += OnComplete;
    }

    private void OnComplete()
    {
        var p = GetComponent<ParticleSystem>();
        var module = p.emission;
        module.rateOverTime = 0;
    }
}
