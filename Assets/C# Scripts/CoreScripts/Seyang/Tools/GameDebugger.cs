using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameDebugger : MonoSingleton<GameDebugger>
{
    protected override bool IsDontDestroyOnLoad => true;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Cover.Instance.ChangeScene("Arithmatic",0.3f,0.2f);
        }
       
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Cover.Instance.ChangeScene("Geometry", 0.3f, 0.2f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Cover.Instance.ChangeScene("Ancient", 0.3f, 0.2f);
        }


        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Cover.Instance.ChangeScene("EndScene", 0.3f, 0.2f);
        }
    }
}
