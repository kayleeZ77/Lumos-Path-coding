using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionBManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource backgroundMusic;  // 背景音乐播放器
    public AudioSource speakerAudio;     // 喇叭的音效播放器

    [Header("Region and Player")]
    public Collider regionB;             // 区域 B 的触发器
    public Transform player;             // 主角的 Transform

    [Header("Audio Settings")]
    public float targetVolume = 0.1f;    // 背景音乐进入区域后的目标音量（原音量的十分之一）
    public float volumeFadeSpeed = 1f;   // 音量变化速度
    private float originalVolume;        // 背景音乐的原始音量

    private bool isInRegionB = false;    // 主角是否在区域 B 内

    private void Start()
    {
        // 保存背景音乐的原始音量
        if (backgroundMusic != null)
        {
            originalVolume = backgroundMusic.volume;
        }

        // 确保喇叭音效正常播放
        if (speakerAudio != null)
        {
            speakerAudio.spatialBlend = 1f; // 设置为 3D 音效
            speakerAudio.loop = true;      // 循环播放
            speakerAudio.Stop();           // 初始状态不播放
        }

        // 检查绑定
        if (player == null)
        {
            Debug.LogError("Player Transform 未绑定，请检查 Inspector 面板！");
        }

        if (regionB == null)
        {
            Debug.LogError("Region B 未绑定，请检查 Inspector 面板！");
        }
    }

    private void Update()
    {
        // 平滑调整背景音乐音量
        if (backgroundMusic != null)
        {
            float target = isInRegionB ? originalVolume * targetVolume : originalVolume;
            backgroundMusic.volume = Mathf.Lerp(backgroundMusic.volume, target, Time.deltaTime * volumeFadeSpeed);
            Debug.Log($"背景音乐当前音量：{backgroundMusic.volume}");
        }

        // 调整喇叭音效音量（根据距离）
        if (isInRegionB && speakerAudio != null)
        {
            float distance = Vector3.Distance(player.position, speakerAudio.transform.position);
            float maxDistance = speakerAudio.maxDistance;
            speakerAudio.volume = Mathf.Clamp01(1f - (distance / maxDistance));
            Debug.Log($"喇叭音量调整为：{speakerAudio.volume}，主角距离：{distance}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered Region B");
            isInRegionB = true;

            // 播放喇叭音效
            if (speakerAudio != null && !speakerAudio.isPlaying)
            {
                speakerAudio.Play();
                Debug.Log("喇叭音效开始播放");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited Region B");
            isInRegionB = false;

            // 停止喇叭音效
            if (speakerAudio != null && speakerAudio.isPlaying)
            {
                speakerAudio.Stop();
                Debug.Log("喇叭音效停止播放");
            }
        }
    }
}
