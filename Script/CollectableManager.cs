using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CollectableManager : MonoBehaviour
{
    public TMP_Text countText;       // ʹ�� TMP_Text ������ͨ Text
    public AudioSource audioSource; // ���� AudioSource
    public AudioClip collectSound;  // �����ռ���Ч
    public AudioClip sceneTransitionSound; // ���볡���л�ǰ����Ч
    private int totalItems = 3;     // ��������
    private int collectedItems = 0; // ���ռ���������

    private void Start()
    {
        // ��� countText �Ƿ�Ϊ��
        if (countText == null)
        {
            Debug.LogError("CountText δ��ȷ�󶨣����� Inspector ����м�飡");
            return;
        }
        UpdateCountText(); // ��ʼ�������ı�

        if (countText == null)
        {
            Debug.LogError("CountText δ�󶨣����� Inspector ��塣");
        }
        else
        {
            Debug.Log("CountText �ѳɹ��󶨡�");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            collectedItems++; // ���Ӽ���
            UpdateCountText();

            // �����ռ���Ч
            if (audioSource != null && collectSound != null)
            {
                audioSource.PlayOneShot(collectSound);
            }

            Destroy(other.gameObject); // ��������ʧ

            // ����ռ���ɣ��ӳٲ�����Ч���л�����
            if (collectedItems == totalItems)
            {
                if (audioSource != null && sceneTransitionSound != null)
                {
                    // ���ų����л�ǰ����Ч
                    audioSource.PlayOneShot(sceneTransitionSound);
                }

                Invoke("LoadNextScene", 5f); // 5����л�����
            }
        }
    }

    private void UpdateCountText()
    {
        if (countText == null)
        {
            Debug.LogError("CountText δ��ȷ�󶨣����� Inspector ����м�飡");
            return;
        }
        countText.text = $"{collectedItems}/{totalItems}"; // ����UI�ı�
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene("EndScene"); // �л�����һ���������滻Ϊʵ�ʳ�����
    }
}
