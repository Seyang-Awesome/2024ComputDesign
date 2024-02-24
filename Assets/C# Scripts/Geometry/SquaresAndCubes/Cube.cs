using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

//它真的只是个Cube
public class Cube : Listener
{
    private int currentLength;
    private int currentDimension;
    private const float MinLength = 0.1f;

    private Dictionary<int, Vector3> dimensionDic = new()
    {
        {1, new Vector3(1f, MinLength, MinLength)},
        {2, new Vector3(1f, MinLength, 1f)},
        {3, new Vector3(1f, 1f, 1f)},
    };
    protected override void OnPublisherStateChanged(IPublisher publisher, int state, PALEventArgs args)
    {
        if (!(args is SquaresAndCubesArgs squaresAndCubesArgs)) return;
        currentLength = squaresAndCubesArgs.Length;
        currentDimension = squaresAndCubesArgs.Dimension;
        
        base.OnPublisherStateChanged(publisher, state, args);
    }

    //只对特定维度的Scale起作用
    private Vector3 MagicMultiple(Vector3 vector, float multipleNum)
    {
        float x = vector.x.IsFloatsEqual(1f) ? multipleNum : vector.x;
        float y = vector.y.IsFloatsEqual(1f) ? multipleNum : vector.y;
        float z = vector.z.IsFloatsEqual(1f) ? multipleNum : vector.z;
        
        return new Vector3(x, y, z);
    }

    private void UpdateState()
    {
        transform.DOScale(MagicMultiple(dimensionDic[currentDimension] ,currentLength), Consts.ObjectsChangeDuration);
    }
}

