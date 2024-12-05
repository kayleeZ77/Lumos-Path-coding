using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowBehind : MonoBehaviour
{
    public Transform target;        // С����� Transform
    public float distance = 5f;     // �������С����ľ���
    public float height = 2f;       // ������ĸ߶�ƫ��
    public float followSpeed = 5f;  // ������ƶ���ƽ���ٶ�
    public float rotationSpeed = 5f; // �������ת��ƽ���ٶ�

    void LateUpdate()
    {
        if (target == null) return;

        // �����������λ�ã�С��������󷽣�
        Vector3 targetPosition = target.position - target.forward * distance + Vector3.up * height;

        // ƽ���ƶ��������Ŀ��λ��
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // �������������ת��ʹ��ʼ�տ���С����
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);

        // ƽ����ת�����
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}

