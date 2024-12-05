using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseControl : MonoBehaviour
{
    public Transform target;               // �����壨Ŀ�����
    public float mouseSensitivity = 120f;  // ���ˮƽ������
    public float verticalSensitivity = 50f; // ��괹ֱ������
    public float distanceFromTarget = 5f;  // �������������ľ���
    public float heightOffset = 2f;        // ����������������ĸ߶�ƫ��
    public float verticalAngleLimit = 15f; // ��ֱ��ת�ĽǶ����ƣ����¸�15�㣩

    private float yaw = 0f;                // ˮƽ��ת�Ƕ�
    private float pitch = -10f;            // ��ʼ�����Ƕȣ���΢���ӣ�

    void Start()
    {
        // ������겢����
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // ��ʼ�� yaw �� pitch
        yaw = target.eulerAngles.y; // �������巽���ʼ��ˮƽ�Ƕ�
    }

    void LateUpdate()
    {
        // ��ȡ�������
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * verticalSensitivity * Time.deltaTime;

        // ����ˮƽ�ʹ�ֱ�Ƕ�
        yaw += mouseX;                           // ˮƽ��ת
        pitch -= mouseY;                         // ��ֱ��ת����� Y ���ƣ�
        pitch = Mathf.Clamp(pitch, -verticalAngleLimit, verticalAngleLimit); // ���Ƹ����Ƿ�Χ�� ��15 ��

        // �����������λ�úͷ���
        Vector3 direction = new Vector3(0, heightOffset, -distanceFromTarget);
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        transform.position = target.position + rotation * direction;

        // �������ʼ�տ���������
        transform.LookAt(target);

        // ����������ĳ���ʹ�����������ˮƽ�Ƕ�һ�£�
        target.rotation = Quaternion.Euler(0, yaw, 0);
    }
}

