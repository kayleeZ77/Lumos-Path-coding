using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // ���ڲ��� TextMeshPro UI

public class NPCInteraction : MonoBehaviour
{
    public Animator npcAnimator;              // NPC �� Animator
    public string triggerAnimationName = "WaveTrigger"; // Animator �д������Ĳ�������
    public GameObject dialoguePanel;          // �Ի���� Panel
    public TextMeshProUGUI dialogueText;      // �Ի����е��ı�
    public string dialogueContent = "Hey! You��re saying you��re not glowing anymore? No big deal! Just head to the Mushroom Forest, find three glowing mushrooms, and they��ll get you shining again. Good luck!"; // �Ի�����

    private bool isPlayerInRange = false;     // ����Ƿ��ڷ�Χ��
    private bool dialogueActive = false;      // �Ի����Ƿ�������ʾ

    void Start()
    {
        // ȷ���Ի���һ��ʼ�����ص�
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }

        // ��ʼ�� Animator Ϊ Idle ״̬
        if (npcAnimator != null)
        {
            // ��ʽ���� Idle ��������ʼ������״̬��
            npcAnimator.Play("Idle");
            //Debug.Log("Animator initialized successfully, playing Idle animation.");
        }
        else
        {
            //Debug.LogWarning("Animator not assigned in the Inspector!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // �����봥����Χ���Ƿ�������
        if (other.CompareTag("Player"))
        {
            //Debug.Log($"Trigger activated by: {other.name}");
            isPlayerInRange = true;

            // �л� NPC �Ķ���
            if (npcAnimator != null)
            {
                npcAnimator.SetTrigger(triggerAnimationName); // �����������
                //Debug.Log("Trigger activated, changing animation!");
            }

            // ��ʾ�Ի���
            if (dialoguePanel != null && !dialogueActive)
            {
                dialoguePanel.SetActive(true);
                dialogueActive = true; // ��ǶԻ����Ѽ���
                if (dialogueText != null)
                {
                    dialogueText.text = dialogueContent;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // ����뿪��Χ���Ƿ�������
        if (other.CompareTag("Player"))
        {
            //Debug.Log($"Trigger exited by: {other.name}");
            isPlayerInRange = false;

            // ���ضԻ���
            if (dialoguePanel != null)
            {
                dialoguePanel.SetActive(false);
                dialogueActive = false; // ��ǶԻ���������
            }
        }
    }

    void Update()
    {
        // ֻ�е�����ڷ�Χ���ҶԻ��򼤻�ʱ���ܵ���ر�
        if (isPlayerInRange && dialogueActive && Input.GetMouseButtonDown(0)) // 0 ��������
        {
            if (dialoguePanel != null)
            {
                dialoguePanel.SetActive(false);
                dialogueActive = false; // ��ǶԻ���������
            }
        }
    }
}
