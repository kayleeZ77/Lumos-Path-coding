using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExample : MonoBehaviour
{
    public Animator npcAnimator; // �����ɫ�� Animator

    void Start()
    {
        // ��� Animator �Ƿ����
        if (npcAnimator == null)
        {
            npcAnimator = GetComponent<Animator>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �����뷶Χ��������
        {
            Debug.Log("Trigger activated, changing animation!");
            npcAnimator.SetTrigger("WaveTrigger"); // ��������
        }
    }
}
