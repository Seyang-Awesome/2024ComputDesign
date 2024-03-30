using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoSingleton<Player>
{
    protected override bool IsDontDestroyOnLoad => false;
}
