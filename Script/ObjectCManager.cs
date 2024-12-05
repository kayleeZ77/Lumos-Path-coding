using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCManager : MonoBehaviour
{
    [Header("Player Settings")]
    public Transform player;                  // ���� Transform

    [Header("Canvas Settings")]
    public GameObject canvasObject;           // ������ʾ������ Canvas
    public float animationDuration = 6f;      // ������ʱ�����룩

    [Header("Audio Settings")]
    public AudioSource successAudio;          // �ɹ�������Ч
    public AudioSource failAudio;             // ʧ�ܰ�����Ч

    [Header("Key Press Detection")]
    public float detectionInterval = 2f;      // ÿ�� 2 ����һ��
    public float tolerance = 0.2f;            // ǰ�� 0.2 ���ʱ�䴰��
    private float nextDetectionTime;          // ��һ�μ���ʱ��
    private int successCount = 0;             // �ɹ���������
    private bool isDetecting = false;         // �Ƿ����ڼ�ⰴ��

    private void Start()
    {
        // ȷ�� Canvas ����Ϸ��ʼʱ����
        if (canvasObject != null)
        {
            canvasObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Canvas Object is not assigned!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Triggered by: {other.name}");

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player interacted with Object C");

            // ���Ŷ���
            StartAnimation();

            // ��ʼ��ⰴ��
            isDetecting = true;
            nextDetectionTime = Time.time + detectionInterval;
        }
    }

    private void Update()
    {
        if (isDetecting)
        {
            DetectKeyPress();
        }
    }

    private void StartAnimation()
    {
        if (canvasObject != null)
        {
            canvasObject.SetActive(true); // ���� Canvas
            Debug.Log("Canvas activated.");
        }
        else
        {
            Debug.LogError("Canvas Object is not assigned!");
        }
    }

    private void DetectKeyPress()
    {
        // ��ǰʱ��
        float currentTime = Time.time;

        // ����Ƿ�������ļ�ⴰ����
        if (currentTime >= nextDetectionTime - tolerance && currentTime <= nextDetectionTime + tolerance)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Mouse left button pressed within time window.");

                // ���ųɹ���Ч
                if (successAudio != null)
                {
                    successAudio.Play();
                }

                successCount++;
                nextDetectionTime += detectionInterval; // ������һ�μ��ʱ��

                // ����Ƿ�ﵽ�ɹ�����
                if (successCount >= 3)
                {
                    EndDetection();
                }
            }
        }
        else if (currentTime > nextDetectionTime + tolerance)
        {
            // ����ʱ�䴰�ڣ�û�гɹ�����
            Debug.Log("Mouse left button press failed.");

            // ����ʧ����Ч
            if (failAudio != null)
            {
                failAudio.Play();
            }

            nextDetectionTime += detectionInterval; // ������һ�μ��ʱ��
        }
    }

    private void EndDetection()
    {
        Debug.Log("Detection ended. Object C interaction completed.");

        // ֹͣ���
        isDetecting = false;

        // ���� Canvas
        if (canvasObject != null)
        {
            canvasObject.SetActive(false);
        }

        // �������� C
        Destroy(gameObject);
    }
}
