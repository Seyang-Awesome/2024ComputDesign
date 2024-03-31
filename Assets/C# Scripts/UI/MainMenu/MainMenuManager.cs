using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        Cover.Instance.ChangeScene("StartScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
