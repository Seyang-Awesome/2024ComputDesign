using System;
using System.Collections.Generic;
using UnityEngine;

public class AncientDialogInteracter : MonoBehaviour, ITipable
{
    private bool isCanOpen = false;
    [Multiline(15)] public string content;
    public bool isHandleContent;
    public void OnEnter(Player player)
    {
        isCanOpen = true;
    }

    public void OnExit(Player player)
    {
        isCanOpen = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (AncientDialogPanel.Instance.IsShowing)
            {
                AncientDialogPanel.Instance.Hide();
            }
            else
            {
                if(isHandleContent)
                    
                    AncientDialogPanel.Instance.Show(content);
                else
                {
                    AncientDialogPanel.Instance.Show(AddHeadTab(content));
                }
            }
        }
    }
    
    string AddHeadTab(string content)
    {
        string result;
        result = content.Replace("\n", "\n    ");
        result = "    " + result;
        return result;
    }
}

