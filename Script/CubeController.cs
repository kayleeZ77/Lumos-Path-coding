using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public float moveSpeed = 5f;         // 移动速度
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // 获取键盘输入
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // 获取相机的方向
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        // 忽略 Y 轴分量，确保在水平面移动
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // 根据相机方向计算移动
        Vector3 movement = forward * moveVertical + right * moveHorizontal;

        // 使用 MovePosition 进行平滑移动
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

        // 如果需要保持方向稳定，可以锁定旋转
        transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
    }
}
