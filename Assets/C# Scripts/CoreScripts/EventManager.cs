using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public event Action<bool> onPlayerFaceDirectionChanged;//因鼠标移动导致玩家朝向变化的事件
    public static Action onEnemyHit;//弹幕碰到敌人后发生的事件
    public Action onEnemyDie;//敌人死亡
    public Action onPlayerEnterPlayScene;//玩家进入场景
    public Action onPlayerEnterEvent;//玩家触发游戏事件
    //public Action<ItemStack> onInventoryAddItem;//当背包中添加了一个物品栈
    //public Action<ItemStack> onInventoryRemoveItem;//当背包中移除了一个物品栈
    public void PlayerFaceDirectionChanged(bool leftOrRight) => onPlayerFaceDirectionChanged?.Invoke(leftOrRight);
    public void TriggerEnemyHitEvent() => onEnemyHit?.Invoke();
    public void OnPlayerEnterPlayScene() => onPlayerEnterPlayScene?.Invoke();
    public void TriggerEnemyDirEvent()=> onEnemyDie?.Invoke();
    public void OnPlayerEnterEvent() => onPlayerEnterEvent?.Invoke();
}
