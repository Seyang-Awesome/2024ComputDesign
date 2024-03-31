using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    public float sensitivityX = 2f; // X轴旋转灵敏度
    public float sensitivityY = 2f; // Y轴旋转灵敏度
    public float minXAngle = -5f; // X轴旋转最小角度
    public float maxXAngle = 5f; // X轴旋转最大角度
    public float minYAngle = -5f;
    public float maxYAngle = 5f;
    public float smoothness = 5f; // 平滑过渡的速度

    private float rotationX = 0f;
    private float rotationY = 0f;
    private float targetRotationX = 0f;
    private float targetRotationY = 0f;

    void Update()
    {
        // 获取鼠标移动距离
        float mouseX = Input.GetAxis("Mouse X") * sensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY;

        // 计算新的目标旋转角度
        targetRotationX -= mouseY;
        targetRotationY += mouseX;

        // 限制目标旋转角度在[minXAngle, maxXAngle]范围内
        targetRotationX = Mathf.Clamp(targetRotationX, minXAngle, maxXAngle);
        targetRotationY = Mathf.Clamp(targetRotationY, minYAngle, maxYAngle);

        // 平滑过渡到目标旋转角度
        rotationX = Mathf.Lerp(rotationX, targetRotationX, smoothness * Time.deltaTime);
        rotationY = Mathf.Lerp(rotationY, targetRotationY, smoothness * Time.deltaTime);

        // 应用旋转
        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }
}