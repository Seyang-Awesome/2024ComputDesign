using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractController : MonoBehaviour
{
    [SerializeField] private float interactMaxDistance; //射线发♂射的长度
    
    private Player player;
    private Transform originTransform => Camera.main.transform;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Inputs.GetInstance().IsTryToInteract)
        {
            //问为什么要两层嵌套就是性能优化（那么一点点）
            if (Physics.Raycast(originTransform.position, 
                    originTransform.forward, 
                    out RaycastHit hit,
                    interactMaxDistance))
            {
                IInteractable interactable = hit.transform.GetComponent<IInteractable>();
                if (interactable != null)
                    interactable.OnInteract(player);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ITipable[] tipable = other.gameObject.GetComponents<ITipable>();
        if(tipable != null)
        {
            foreach (var t in tipable)
            {
                t.OnEnter(player);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ITipable[] tipable = other.gameObject.GetComponents<ITipable>();
        if(tipable != null)
        {
            foreach (var t in tipable)
            {
                t.OnExit(player);
            }
        }
    }
}

