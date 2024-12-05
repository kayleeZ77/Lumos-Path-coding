using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWithCameraController : MonoBehaviour
{
    public float moveSpeed = 5f;              // 角色移动速度
    public float rotationSpeed = 10f;        // 角色旋转速度
    public Transform cameraTransform;        // 主摄像机
    public float cameraDistance = 15f;       // 摄像机与角色的距离
    public float cameraHeight = 5f;          // 摄像机的高度
    public float mouseSensitivity = 150f;    // 鼠标灵敏度
    public float verticalAngleLimit = 30f;   // 摄像机俯仰角限制

    private Animator animator;               // 动画控制器
    private Rigidbody rb;                    // 刚体组件
    private float pitch = 0f;                // 摄像机俯仰角

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        // 锁定鼠标
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // 获取鼠标输入
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // 更新摄像机水平旋转和俯仰角
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -verticalAngleLimit, verticalAngleLimit);

        // 摄像机的旋转
        Quaternion cameraRotation = Quaternion.Euler(pitch, cameraTransform.eulerAngles.y + mouseX, 0);
        cameraTransform.rotation = cameraRotation;

        // 更新摄像机位置
        Vector3 cameraOffset = cameraTransform.rotation * new Vector3(0, cameraHeight, -cameraDistance);
        cameraTransform.position = transform.position + cameraOffset;
    }

    void FixedUpdate()
    {
        // 获取键盘输入
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // 基于摄像机朝向计算角色移动方向
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // 忽略 Y 轴分量
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // 最终移动方向
        Vector3 movement = forward * moveVertical + right * moveHorizontal;

        // 如果有输入，旋转角色
        if (movement.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // 移动角色
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.deltaTime);

        // 动画控制
        if (animator != null)
        {
            animator.SetFloat("Speed", movement.magnitude); // 根据移动速度设置动画
        }
    }
}
