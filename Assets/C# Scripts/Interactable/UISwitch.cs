using System;
using System.Collections.Generic;
using UnityEngine;

public class UISwitch : Switch
{
    public void SwitchState()
    {
        IsOn = !IsOn;
        if (IsOn) SwitchOff();
        else SwitchOn();
    }
}

