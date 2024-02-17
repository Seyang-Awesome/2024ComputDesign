using System;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void OnInteractStart(Player player);
    void OnInteractFinished(Player player);
    
    
}

