using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public float moveSpeed = 5f;         // �ƶ��ٶ�
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // ��ȡ��������
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // ��ȡ����ķ���
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        // ���� Y �������ȷ����ˮƽ���ƶ�
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // ���������������ƶ�
        Vector3 movement = forward * moveVertical + right * moveHorizontal;

        // ʹ�� MovePosition ����ƽ���ƶ�
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

        // �����Ҫ���ַ����ȶ�������������ת
        transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
    }
}
