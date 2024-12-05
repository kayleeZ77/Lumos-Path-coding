using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWithCameraController : MonoBehaviour
{
    public float moveSpeed = 5f;              // ��ɫ�ƶ��ٶ�
    public float rotationSpeed = 10f;        // ��ɫ��ת�ٶ�
    public Transform cameraTransform;        // �������
    public float cameraDistance = 15f;       // ��������ɫ�ľ���
    public float cameraHeight = 5f;          // ������ĸ߶�
    public float mouseSensitivity = 150f;    // ���������
    public float verticalAngleLimit = 30f;   // ���������������

    private Animator animator;               // ����������
    private Rigidbody rb;                    // �������
    private float pitch = 0f;                // �����������

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        // �������
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // ��ȡ�������
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // ���������ˮƽ��ת�͸�����
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -verticalAngleLimit, verticalAngleLimit);

        // ���������ת
        Quaternion cameraRotation = Quaternion.Euler(pitch, cameraTransform.eulerAngles.y + mouseX, 0);
        cameraTransform.rotation = cameraRotation;

        // ���������λ��
        Vector3 cameraOffset = cameraTransform.rotation * new Vector3(0, cameraHeight, -cameraDistance);
        cameraTransform.position = transform.position + cameraOffset;
    }

    void FixedUpdate()
    {
        // ��ȡ��������
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // �����������������ɫ�ƶ�����
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // ���� Y �����
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // �����ƶ�����
        Vector3 movement = forward * moveVertical + right * moveHorizontal;

        // ��������룬��ת��ɫ
        if (movement.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // �ƶ���ɫ
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.deltaTime);

        // ��������
        if (animator != null)
        {
            animator.SetFloat("Speed", movement.magnitude); // �����ƶ��ٶ����ö���
        }
    }
}
