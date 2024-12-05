using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    public Button startButton;       // "Start Game" ��ť
    public Button playAgainButton;   // "Play Again" ��ť
    public AudioClip clickSound;     // �������
    private AudioSource audioSource; // ��Ƶ������

    void Start()
    {
        // ��ȡ AudioSource ������Զ����
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // �� "Start Game" ��ť����¼�
        if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartButtonClick);
        }

        // �� "Play Again" ��ť����¼�
        if (playAgainButton != null)
        {
            playAgainButton.onClick.AddListener(OnPlayAgainButtonClick);
        }
    }

    void OnStartButtonClick()
    {
        // ���ŵ������
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }

        // �л��� PlayScene
        SceneManager.LoadScene("PlayScene");
    }

    void OnPlayAgainButtonClick()
    {
        // ���ŵ������
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }

        // �л��� StartScene
        SceneManager.LoadScene("StartScene");
    }
}
