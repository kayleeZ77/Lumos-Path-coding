using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelAnimationManager : MonoBehaviour
{
    [Header("Panel Settings")]
    public GameObject animationPanel;        // Panel 对象
    public float animationDuration = 5f;     // 动画持续时间

    private Animator animator;               // Panel 的 Animator

    private void Start()
    {
        // 确保 Panel 在游戏开始时隐藏
        if (animationPanel != null)
        {
            animationPanel.SetActive(false);
            animator = animationPanel.GetComponent<Animator>();
        }
    }

    // 触发动画
    public void StartPanelAnimation()
    {
        if (animationPanel != null)
        {
            animationPanel.SetActive(true);  // 显示 Panel

            if (animator != null)
            {
                animator.Play("PanelAnimation"); // 播放动画
            }

            // 在动画结束后隐藏 Panel
            Invoke("HidePanel", animationDuration);
        }
    }

    // 隐藏 Panel
    private void HidePanel()
    {
        if (animationPanel != null)
        {
            animationPanel.SetActive(false); // 隐藏 Panel
        }
    }
}
