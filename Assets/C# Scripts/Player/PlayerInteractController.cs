using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractController : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void OnCollisionEnter(Collision other)
    {
        ITipable tipable = other.gameObject.GetComponent<ITipable>();
        if(tipable != null)
            tipable.OnEnter(player);
    }

    private void OnCollisionExit(Collision other)
    {
        ITipable tipable = other.gameObject.GetComponent<ITipable>();
        if(tipable != null)
            tipable.OnExit(player);
    }
}

