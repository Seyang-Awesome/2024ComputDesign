using System;
using System.Collections.Generic;
using UnityEngine;

public interface ITipable
{
    void OnEnter(Player player);
    void OnExit(Player player);
}

