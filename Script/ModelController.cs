using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    public float moveSpeed = 5f;         // 移动速度
    private Rigidbody rb;               // 刚体组件
    private Animator animator;          // 动画控制器

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // 获取键盘输入
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // 计算移动方向
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        // 控制刚体移动
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

        // 控制模型朝向移动方向
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement); // 目标方向
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * 10f); // 平滑旋转
        }

        // 设置动画参数
        if (animator != null)
        {
            float speed = movement.magnitude * moveSpeed; // 计算速度
            animator.SetFloat("Speed", speed);           // 动态设置动画参数
        }
    }
}
