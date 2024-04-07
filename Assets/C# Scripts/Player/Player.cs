using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoSingleton<Player>
{
    protected override bool IsDontDestroyOnLoad => false;

    private void Update()
    {
        if (transform.position.y < -100)
            Cover.Instance.ChangeScene(SceneManager.GetActiveScene().name);
    }
}
