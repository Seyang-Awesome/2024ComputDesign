using UnityEngine;

/// <summary>
/// GPT酱写的，不关我事喵不关我事
/// </summary>
public class FirstPersonController : MonoBehaviour
{
    public float rotationSpeed = 5.0f; // 旋转速度
    public float moveSpeed = 5.0f; // 移动速度
    public float pitchRange = 80.0f; // 俯仰角限制

    private float pitch = 0.0f;

    void Update()
    {
        // 获取鼠标输入
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = -Input.GetAxis("Mouse Y") * rotationSpeed; // 反转Y轴输入

        // 旋转玩家对象（左右旋转）
        transform.Rotate(0.0f, mouseX, 0.0f);

        // 旋转相机对象（上下旋转）
        pitch += mouseY;
        pitch = Mathf.Clamp(pitch, -pitchRange, pitchRange); // 限制俯仰角
        Camera.main.transform.localRotation = Quaternion.Euler(pitch, 0.0f, 0.0f);

        // 获取移动输入
        float moveX = Input.GetAxis("Horizontal") * moveSpeed;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed;

        // 根据相机的前进方向计算移动方向
        Vector3 moveDirection = Camera.main.transform.forward * moveZ;
        moveDirection += Camera.main.transform.right * moveX;
        moveDirection.y = 0.0f; // 确保不进行垂直移动

        // 移动玩家对象
        transform.position += moveDirection * Time.deltaTime;
    }
}