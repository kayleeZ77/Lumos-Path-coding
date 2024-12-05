using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCManager : MonoBehaviour
{
    [Header("Player Settings")]
    public Transform player;                  // 主角 Transform

    [Header("Canvas Settings")]
    public GameObject canvasObject;           // 用于显示动画的 Canvas
    public float animationDuration = 6f;      // 动画总时长（秒）

    [Header("Audio Settings")]
    public AudioSource successAudio;          // 成功按键音效
    public AudioSource failAudio;             // 失败按键音效

    [Header("Key Press Detection")]
    public float detectionInterval = 2f;      // 每隔 2 秒检测一次
    public float tolerance = 0.2f;            // 前后 0.2 秒的时间窗口
    private float nextDetectionTime;          // 下一次检测的时间
    private int successCount = 0;             // 成功按键次数
    private bool isDetecting = false;         // 是否正在检测按键

    private void Start()
    {
        // 确保 Canvas 在游戏开始时隐藏
        if (canvasObject != null)
        {
            canvasObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Canvas Object is not assigned!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Triggered by: {other.name}");

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player interacted with Object C");

            // 播放动画
            StartAnimation();

            // 开始检测按键
            isDetecting = true;
            nextDetectionTime = Time.time + detectionInterval;
        }
    }

    private void Update()
    {
        if (isDetecting)
        {
            DetectKeyPress();
        }
    }

    private void StartAnimation()
    {
        if (canvasObject != null)
        {
            canvasObject.SetActive(true); // 激活 Canvas
            Debug.Log("Canvas activated.");
        }
        else
        {
            Debug.LogError("Canvas Object is not assigned!");
        }
    }

    private void DetectKeyPress()
    {
        // 当前时间
        float currentTime = Time.time;

        // 检查是否在允许的检测窗口内
        if (currentTime >= nextDetectionTime - tolerance && currentTime <= nextDetectionTime + tolerance)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Mouse left button pressed within time window.");

                // 播放成功音效
                if (successAudio != null)
                {
                    successAudio.Play();
                }

                successCount++;
                nextDetectionTime += detectionInterval; // 更新下一次检测时间

                // 检查是否达到成功次数
                if (successCount >= 3)
                {
                    EndDetection();
                }
            }
        }
        else if (currentTime > nextDetectionTime + tolerance)
        {
            // 超过时间窗口，没有成功按下
            Debug.Log("Mouse left button press failed.");

            // 播放失败音效
            if (failAudio != null)
            {
                failAudio.Play();
            }

            nextDetectionTime += detectionInterval; // 更新下一次检测时间
        }
    }

    private void EndDetection()
    {
        Debug.Log("Detection ended. Object C interaction completed.");

        // 停止检测
        isDetecting = false;

        // 隐藏 Canvas
        if (canvasObject != null)
        {
            canvasObject.SetActive(false);
        }

        // 销毁物体 C
        Destroy(gameObject);
    }
}
