using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StepTrigger : MonoBehaviour
{
    public UnityEvent OnTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        OnTrigger?.Invoke();
        enabled = false;
    }
}

