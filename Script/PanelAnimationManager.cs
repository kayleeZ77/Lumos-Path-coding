using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelAnimationManager : MonoBehaviour
{
    [Header("Panel Settings")]
    public GameObject animationPanel;        // Panel ����
    public float animationDuration = 5f;     // ��������ʱ��

    private Animator animator;               // Panel �� Animator

    private void Start()
    {
        // ȷ�� Panel ����Ϸ��ʼʱ����
        if (animationPanel != null)
        {
            animationPanel.SetActive(false);
            animator = animationPanel.GetComponent<Animator>();
        }
    }

    // ��������
    public void StartPanelAnimation()
    {
        if (animationPanel != null)
        {
            animationPanel.SetActive(true);  // ��ʾ Panel

            if (animator != null)
            {
                animator.Play("PanelAnimation"); // ���Ŷ���
            }

            // �ڶ������������� Panel
            Invoke("HidePanel", animationDuration);
        }
    }

    // ���� Panel
    private void HidePanel()
    {
        if (animationPanel != null)
        {
            animationPanel.SetActive(false); // ���� Panel
        }
    }
}
