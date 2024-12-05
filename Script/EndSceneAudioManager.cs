using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneAudioManager : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource audioSource;   // ��Ƶ�������
    public AudioClip endSceneClip;    // Ҫ���ŵ���Ƶ����

    private void Start()
    {
        // ����Ƿ������ƵԴ����Ƶ����
        if (audioSource != null && endSceneClip != null)
        {
            audioSource.clip = endSceneClip; // ������Ƶ����
            audioSource.Play();              // ������Ƶ
            Debug.Log("EndScene audio started playing.");
        }
        else
        {
            Debug.LogError("AudioSource or AudioClip is not assigned!");
        }
    }
}
