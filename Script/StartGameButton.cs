using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    public Button startButton;       // "Start Game" 按钮
    public Button playAgainButton;   // "Play Again" 按钮
    public AudioClip clickSound;     // 点击声音
    private AudioSource audioSource; // 音频播放器

    void Start()
    {
        // 获取 AudioSource 组件或自动添加
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // 绑定 "Start Game" 按钮点击事件
        if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartButtonClick);
        }

        // 绑定 "Play Again" 按钮点击事件
        if (playAgainButton != null)
        {
            playAgainButton.onClick.AddListener(OnPlayAgainButtonClick);
        }
    }

    void OnStartButtonClick()
    {
        // 播放点击声音
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }

        // 切换到 PlayScene
        SceneManager.LoadScene("PlayScene");
    }

    void OnPlayAgainButtonClick()
    {
        // 播放点击声音
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }

        // 切换到 StartScene
        SceneManager.LoadScene("StartScene");
    }
}
