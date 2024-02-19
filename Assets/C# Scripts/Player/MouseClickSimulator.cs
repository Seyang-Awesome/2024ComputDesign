using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//使用Cursor.lockState = CursorLockMode.Locked后
//鼠标位置被固定在屏幕中央并且不知道为什么不能进行点击事件
//故请出ChatGPT来帮忙写出此脚本手动模拟点击事件
public class MouseClickSimulator : MonoBehaviour
{
    void Update()
    {
        // 模拟左键点击
        if (Inputs.GetInstance().IsMouseRightDown)
        {
            SimulateMouseClick();
        }
    }

    void SimulateMouseClick()
    {
        // 获取当前鼠标位置
        Vector2 mousePosition = Input.mousePosition;

        // 创建PointerEventData
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);

        // 设置鼠标位置
        pointerEventData.position = Input.mousePosition;
        
        // 获取点击到的所有对象
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        // 遍历点击到的对象并执行点击事件
        foreach (var hit in raycastResults)
        {
            ExecuteEvents.Execute(hit.gameObject, pointerEventData, ExecuteEvents.pointerClickHandler);
        }
    }
}

