using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExample : MonoBehaviour
{
    public Animator npcAnimator; // 分配角色的 Animator

    void Start()
    {
        // 检查 Animator 是否存在
        if (npcAnimator == null)
        {
            npcAnimator = GetComponent<Animator>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 检查进入范围的是主角
        {
            Debug.Log("Trigger activated, changing animation!");
            npcAnimator.SetTrigger("WaveTrigger"); // 触发动画
        }
    }
}
