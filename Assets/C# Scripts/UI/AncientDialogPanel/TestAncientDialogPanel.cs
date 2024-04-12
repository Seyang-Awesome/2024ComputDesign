using System;
using System.Collections.Generic;
using UnityEngine;

public class TestAncientDialogPanel : MonoBehaviour
{
    [SerializeField] private string content;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            AncientDialogPanel.Instance.Show(content);
        }
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            AncientDialogPanel.Instance.Hide();
        }
    }
}

