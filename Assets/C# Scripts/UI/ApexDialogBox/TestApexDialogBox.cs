using System;
using System.Collections.Generic;
using UnityEngine;

public class TestApexDialogBox : MonoBehaviour
{
    public ApexDialogBoxGroup[] groups;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            foreach (var group in groups)
            {
                ApexDialogBoxPanel.Instance.PushDialogContent(group);
            }
        }
    }
}

