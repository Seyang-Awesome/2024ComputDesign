using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbacusDecorationManager : MonoBehaviour
{
    [SerializeField]
    private GameObject particle;

    private Abacus parent;
    private int count = 0;

    private void Awake()
    {
        parent = GetComponentInParent<Abacus>();
        parent.onFinishLevel += Effect;
        parent.onFinishGame += OnComplete;
    }

    private void Effect()
    {
        var dec = transform.GetChild(count++);
        dec.gameObject.SetActive(true);
        Instantiate(particle, dec.position, Quaternion.identity);
    }

    private void OnComplete()
    {
        foreach(var c in GetComponentsInChildren<AbacusDecoration>())
        {
            c.end = true;
        }
    }
}
