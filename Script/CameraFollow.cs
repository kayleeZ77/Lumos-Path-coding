using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowBehind : MonoBehaviour
{
    public Transform target;        // 小方块的 Transform
    public float distance = 5f;     // 摄像机与小方块的距离
    public float height = 2f;       // 摄像机的高度偏移
    public float followSpeed = 5f;  // 摄像机移动的平滑速度
    public float rotationSpeed = 5f; // 摄像机旋转的平滑速度

    void LateUpdate()
    {
        if (target == null) return;

        // 计算摄像机的位置（小方块的正后方）
        Vector3 targetPosition = target.position - target.forward * distance + Vector3.up * height;

        // 平滑移动摄像机到目标位置
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // 计算摄像机的旋转，使其始终看向小方块
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);

        // 平滑旋转摄像机
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}

