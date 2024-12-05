using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CollectableManager : MonoBehaviour
{
    public TMP_Text countText;       // 使用 TMP_Text 代替普通 Text
    public AudioSource audioSource; // 拖入 AudioSource
    public AudioClip collectSound;  // 拖入收集音效
    public AudioClip sceneTransitionSound; // 拖入场景切换前的音效
    private int totalItems = 3;     // 物体总数
    private int collectedItems = 0; // 已收集物体数量

    private void Start()
    {
        // 检查 countText 是否为空
        if (countText == null)
        {
            Debug.LogError("CountText 未正确绑定，请在 Inspector 面板中检查！");
            return;
        }
        UpdateCountText(); // 初始化计数文本

        if (countText == null)
        {
            Debug.LogError("CountText 未绑定！请检查 Inspector 面板。");
        }
        else
        {
            Debug.Log("CountText 已成功绑定。");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            collectedItems++; // 增加计数
            UpdateCountText();

            // 播放收集音效
            if (audioSource != null && collectSound != null)
            {
                audioSource.PlayOneShot(collectSound);
            }

            Destroy(other.gameObject); // 让物体消失

            // 如果收集完成，延迟播放音效和切换场景
            if (collectedItems == totalItems)
            {
                if (audioSource != null && sceneTransitionSound != null)
                {
                    // 播放场景切换前的音效
                    audioSource.PlayOneShot(sceneTransitionSound);
                }

                Invoke("LoadNextScene", 5f); // 5秒后切换场景
            }
        }
    }

    private void UpdateCountText()
    {
        if (countText == null)
        {
            Debug.LogError("CountText 未正确绑定，请在 Inspector 面板中检查！");
            return;
        }
        countText.text = $"{collectedItems}/{totalItems}"; // 更新UI文本
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene("EndScene"); // 切换到另一个场景，替换为实际场景名
    }
}
