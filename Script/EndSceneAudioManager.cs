using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneAudioManager : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource audioSource;   // 音频播放组件
    public AudioClip endSceneClip;    // 要播放的音频剪辑

    private void Start()
    {
        // 检查是否绑定了音频源和音频剪辑
        if (audioSource != null && endSceneClip != null)
        {
            audioSource.clip = endSceneClip; // 设置音频剪辑
            audioSource.Play();              // 播放音频
            Debug.Log("EndScene audio started playing.");
        }
        else
        {
            Debug.LogError("AudioSource or AudioClip is not assigned!");
        }
    }
}
