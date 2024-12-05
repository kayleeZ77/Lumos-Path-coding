using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    public float moveSpeed = 5f;         // �ƶ��ٶ�
    private Rigidbody rb;               // �������
    private Animator animator;          // ����������

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // ��ȡ��������
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // �����ƶ�����
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        // ���Ƹ����ƶ�
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

        // ����ģ�ͳ����ƶ�����
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement); // Ŀ�귽��
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * 10f); // ƽ����ת
        }

        // ���ö�������
        if (animator != null)
        {
            float speed = movement.magnitude * moveSpeed; // �����ٶ�
            animator.SetFloat("Speed", speed);           // ��̬���ö�������
        }
    }
}
