using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 突发奇想，就是想用MVC，别骂了
/// </summary>
public class CuttingCircleController : MonoBehaviour
{
    [SerializeField] private CuttingCircleView view;
    [SerializeField] private CuttingCircleModel model;
    private bool isPlayerEnter = false;
    private void Awake()
    {
        view.OnClickPanel += OnClickView;
        model.OnCurrentModelChanged += OnModelUpdate;
    }

    private void OnDestroy()
    {
        view.OnClickPanel += OnClickView;
        model.OnCurrentModelChanged += OnModelUpdate;
    }

    /// <summary>
    /// 不要动秋梨膏，外部有引用
    /// </summary>
    public void OnPlayerEnter()
    {
        isPlayerEnter = true;
    }

    public void OnClickView()
    {
        if (!isPlayerEnter) return;
        model.MoveNext();
    }

    public void OnModelUpdate(CuttingCircleData data)
    {
        view.OnModelUpdate(data);
    }
}

