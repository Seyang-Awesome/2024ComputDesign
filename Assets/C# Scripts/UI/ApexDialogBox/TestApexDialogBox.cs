using System;
using System.Collections.Generic;
using UnityEngine;

public class TestApexDialogBox : MonoBehaviour
{
    public ApexDialogBoxGroup group;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ApexDialogBoxPanel.Instance.PushDialogContent(group);
        }
    }
}

