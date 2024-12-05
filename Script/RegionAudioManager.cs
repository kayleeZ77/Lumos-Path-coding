using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionAudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource backgroundMusic;  // 背景音乐播放器
    public AudioSource regionAudio;      // 区域音效播放器

    [Header("Region and Player")]
    public Collider regionA;             // 区域 A 的触发器
    public Transform player;             // 主角的 Transform

    [Header("Audio Settings")]
    [Range(0f, 1f)] public float backgroundVolume = 0.5f; // 背景音乐的音量
    [Range(0f, 1f)] public float regionVolume = 1f;       // 区域音效的音量

    private bool isInRegionA = false;    // 主角是否在区域 A 内
    private bool isMoving = false;       // 主角是否在移动
    private Vector3 lastPosition;        // 上一帧主角的位置

    private void Start()
    {
        // 初始化背景音乐
        if (backgroundMusic != null)
        {
            backgroundMusic.volume = backgroundVolume; // 设置背景音乐音量
            backgroundMusic.Play();
            Debug.Log("背景音乐开始播放，音量设置为：" + backgroundVolume);
        }

        // 初始化区域音效
        if (regionAudio != null)
        {
            regionAudio.volume = regionVolume; // 设置区域音效音量
        }

        // 检查绑定
        if (player == null)
        {
            Debug.LogError("Player Transform 未绑定，请检查 Inspector 面板！");
        }

        if (regionA == null)
        {
            Debug.LogError("Region A 未绑定，请检查 Inspector 面板！");
        }
    }

    private void Update()
    {
        if (player == null || !isInRegionA) return;

        // 检测主角是否在移动
        isMoving = Vector3.Distance(player.position, lastPosition) > 0.01f;
        lastPosition = player.position;

        // 根据主角的移动状态控制区域音效
        if (isMoving)
        {
            if (!regionAudio.isPlaying)
            {
                regionAudio.Play();
                Debug.Log("区域音效开始播放（主角移动）");
            }
        }
        else
        {
            if (regionAudio.isPlaying)
            {
                regionAudio.Pause();
                Debug.Log("区域音效暂停播放（主角未移动）");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Object entered trigger: {other.name}, Tag: {other.tag}");

        // 如果触发器是区域 A
        if (other.gameObject == regionA.gameObject)
        {
            Debug.Log("主角进入区域 A");
            isInRegionA = true;

            // 暂停背景音乐
            if (backgroundMusic != null && backgroundMusic.isPlaying)
            {
                backgroundMusic.Pause();
                Debug.Log("背景音乐暂停");
            }
            return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"Object exited trigger: {other.name}, Tag: {other.tag}");

        // 如果触发器是区域 A
        if (other.gameObject == regionA.gameObject)
        {
            Debug.Log("主角离开区域 A");
            isInRegionA = false;

            // 恢复背景音乐
            if (backgroundMusic != null && !backgroundMusic.isPlaying)
            {
                backgroundMusic.Play();
                Debug.Log("背景音乐恢复播放");
            }

            // 停止区域音效
            if (regionAudio.isPlaying)
            {
                regionAudio.Stop();
                Debug.Log("区域音效停止播放");
            }
        }
    }

    // 动态调整背景音乐音量
    public void SetBackgroundVolume(float volume)
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.volume = volume;
            Debug.Log("背景音乐音量调整为：" + volume);
        }
    }

    // 动态调整区域音效音量
    public void SetRegionVolume(float volume)
    {
        if (regionAudio != null)
        {
            regionAudio.volume = volume;
            Debug.Log("区域音效音量调整为：" + volume);
        }
    }
}
