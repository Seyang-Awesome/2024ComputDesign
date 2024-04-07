using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        AudioManager.Instance.PlayBgm("Space");
    }

    public void StartGame()
    {
        Cover.Instance.ChangeScene("StartScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
