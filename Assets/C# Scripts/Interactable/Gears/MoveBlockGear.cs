using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoveBlockGear : Gear
{
    [SerializeField] private List<MoveBlock> moveBlocks = new();

    protected override void SwitchOn()
    {
        base.SwitchOn();
        
        Sequence switchOn = DOTween.Sequence();
        moveBlocks.ForEach(moveBlock =>
        {
            switchOn
                .AppendCallback(() => moveBlock.MoveToTarget())
                .AppendCallback(() => Camera.main.DOShakePosition(Consts.BlockMoveDuration,Consts.BlockMoveShakeIntensity,100))
                .AppendInterval(Consts.BlockMoveInterval);
        });
    }
    
    protected override void SwitchOff()
    {
        base.SwitchOff();
        
        Sequence switchOn = DOTween.Sequence();
        moveBlocks.ForEach(moveBlock =>
        {
            switchOn
                .AppendCallback(() => moveBlock.MoveToOrigin())
                .AppendCallback(() => Camera.main.DOShakePosition(Consts.BlockMoveDuration,Consts.BlockMoveShakeIntensity,100))
                .AppendInterval(Consts.BlockMoveInterval);
        });
    }

    #region Test

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
            SwitchOn();
        
        if(Input.GetKeyDown(KeyCode.F))
            SwitchOff();
    }

    #endregion
}

