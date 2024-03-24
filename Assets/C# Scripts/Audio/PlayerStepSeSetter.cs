using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStepSeSetter : MonoBehaviour
{
    public AudioRandomGroup audioRandomGroup;
    private void Awake()
    {
        FirstPersonController.Instance.audioRandomGroup = audioRandomGroup;
        Destroy(gameObject);
    }
}

