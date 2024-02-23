using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform mainCameraTransform;

    private void Start()
    {
        // 获取主摄像机的引用
        mainCameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        // 计算广告牌物体朝向摄像机的旋转
        transform.forward = transform.position - mainCameraTransform.position ;
    }
}