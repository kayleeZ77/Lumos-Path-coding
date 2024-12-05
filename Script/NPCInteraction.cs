using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // 用于操作 TextMeshPro UI

public class NPCInteraction : MonoBehaviour
{
    public Animator npcAnimator;              // NPC 的 Animator
    public string triggerAnimationName = "WaveTrigger"; // Animator 中触发器的参数名称
    public GameObject dialoguePanel;          // 对话框的 Panel
    public TextMeshProUGUI dialogueText;      // 对话框中的文本
    public string dialogueContent = "Hey! You’re saying you’re not glowing anymore? No big deal! Just head to the Mushroom Forest, find three glowing mushrooms, and they’ll get you shining again. Good luck!"; // 对话内容

    private bool isPlayerInRange = false;     // 玩家是否在范围内
    private bool dialogueActive = false;      // 对话框是否正在显示

    void Start()
    {
        // 确保对话框一开始是隐藏的
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }

        // 初始化 Animator 为 Idle 状态
        if (npcAnimator != null)
        {
            // 显式播放 Idle 动画（初始化动画状态）
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
        // 检测进入触发范围的是否是主角
        if (other.CompareTag("Player"))
        {
            //Debug.Log($"Trigger activated by: {other.name}");
            isPlayerInRange = true;

            // 切换 NPC 的动画
            if (npcAnimator != null)
            {
                npcAnimator.SetTrigger(triggerAnimationName); // 激活动画触发器
                //Debug.Log("Trigger activated, changing animation!");
            }

            // 显示对话框
            if (dialoguePanel != null && !dialogueActive)
            {
                dialoguePanel.SetActive(true);
                dialogueActive = true; // 标记对话框已激活
                if (dialogueText != null)
                {
                    dialogueText.text = dialogueContent;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // 检测离开范围的是否是主角
        if (other.CompareTag("Player"))
        {
            //Debug.Log($"Trigger exited by: {other.name}");
            isPlayerInRange = false;

            // 隐藏对话框
            if (dialoguePanel != null)
            {
                dialoguePanel.SetActive(false);
                dialogueActive = false; // 标记对话框已隐藏
            }
        }
    }

    void Update()
    {
        // 只有当玩家在范围内且对话框激活时才能点击关闭
        if (isPlayerInRange && dialogueActive && Input.GetMouseButtonDown(0)) // 0 是鼠标左键
        {
            if (dialoguePanel != null)
            {
                dialoguePanel.SetActive(false);
                dialogueActive = false; // 标记对话框已隐藏
            }
        }
    }
}
