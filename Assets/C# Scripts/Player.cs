using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //偶牛皮怎么项目里没单例基类的，主播乱写一个了
    private static Player instance;
    public static Player Instance { get { return instance; } }

    private void Awake()
    {
        instance = this;
    }
}
