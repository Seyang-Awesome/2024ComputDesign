using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// 突发奇想，就是想用MVC，别骂了
/// </summary>
public class CuttingCircleController : MonoBehaviour
{
    [SerializeField] private CuttingCircleView view;
    [SerializeField] private CuttingCircleModel model;
    [SerializeField] private ApexDialogBoxGroup group;
    private bool isPlayerEnter = false;
    private void Awake()
    {
        view.OnClickPanel += OnClickView;
        model.OnCurrentModelChanged += OnModelUpdate;
        model.OnLastModelDequeue += OnLastModel;
    }

    private void OnDestroy()
    {
        view.OnClickPanel -= OnClickView;
        model.OnCurrentModelChanged -= OnModelUpdate;
        model.OnLastModelDequeue -= OnLastModel;
    }

    /// <summary>
    /// 不要动秋梨膏，外部有引用
    /// </summary>
    public void OnPlayerEnter()
    {
        isPlayerEnter = true;
    }

    private void OnClickView()
    {
        if (!isPlayerEnter) return;
        model.MoveNext();
    }

    private void OnModelUpdate(CuttingCircleData data)
    {
        view.OnModelUpdate(data);
    }

    private void OnLastModel()
    {
        DOTween.Sequence()
            .AppendInterval(group.clip.length + 1)
            .AppendCallback(() =>
            {
                Cover.Instance.SetBackgroundColor(Color.white);
                Cover.Instance.ChangeScene("EndScene", group.clip.length + 1);
            });
        
        ApexDialogBoxPanel.Instance.PushDialogContent(group);
    }
}

