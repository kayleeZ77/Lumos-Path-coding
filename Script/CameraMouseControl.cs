using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseControl : MonoBehaviour
{
    public Transform target;               // 正方体（目标对象）
    public float mouseSensitivity = 120f;  // 鼠标水平灵敏度
    public float verticalSensitivity = 50f; // 鼠标垂直灵敏度
    public float distanceFromTarget = 5f;  // 摄像机与正方体的距离
    public float heightOffset = 2f;        // 摄像机相对于正方体的高度偏移
    public float verticalAngleLimit = 15f; // 垂直旋转的角度限制（上下各15°）

    private float yaw = 0f;                // 水平旋转角度
    private float pitch = -10f;            // 初始俯仰角度（轻微俯视）

    void Start()
    {
        // 锁定鼠标并隐藏
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 初始化 yaw 和 pitch
        yaw = target.eulerAngles.y; // 以正方体方向初始化水平角度
    }

    void LateUpdate()
    {
        // 获取鼠标输入
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * verticalSensitivity * Time.deltaTime;

        // 更新水平和垂直角度
        yaw += mouseX;                           // 水平旋转
        pitch -= mouseY;                         // 垂直旋转（鼠标 Y 控制）
        pitch = Mathf.Clamp(pitch, -verticalAngleLimit, verticalAngleLimit); // 限制俯仰角范围在 ±15 度

        // 计算摄像机的位置和方向
        Vector3 direction = new Vector3(0, heightOffset, -distanceFromTarget);
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        transform.position = target.position + rotation * direction;

        // 让摄像机始终看向正方体
        transform.LookAt(target);

        // 更新正方体的朝向（使其与摄像机的水平角度一致）
        target.rotation = Quaternion.Euler(0, yaw, 0);
    }
}

