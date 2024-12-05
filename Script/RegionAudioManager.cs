using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionAudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource backgroundMusic;  // �������ֲ�����
    public AudioSource regionAudio;      // ������Ч������

    [Header("Region and Player")]
    public Collider regionA;             // ���� A �Ĵ�����
    public Transform player;             // ���ǵ� Transform

    [Header("Audio Settings")]
    [Range(0f, 1f)] public float backgroundVolume = 0.5f; // �������ֵ�����
    [Range(0f, 1f)] public float regionVolume = 1f;       // ������Ч������

    private bool isInRegionA = false;    // �����Ƿ������� A ��
    private bool isMoving = false;       // �����Ƿ����ƶ�
    private Vector3 lastPosition;        // ��һ֡���ǵ�λ��

    private void Start()
    {
        // ��ʼ����������
        if (backgroundMusic != null)
        {
            backgroundMusic.volume = backgroundVolume; // ���ñ�����������
            backgroundMusic.Play();
            Debug.Log("�������ֿ�ʼ���ţ���������Ϊ��" + backgroundVolume);
        }

        // ��ʼ��������Ч
        if (regionAudio != null)
        {
            regionAudio.volume = regionVolume; // ����������Ч����
        }

        // ����
        if (player == null)
        {
            Debug.LogError("Player Transform δ�󶨣����� Inspector ��壡");
        }

        if (regionA == null)
        {
            Debug.LogError("Region A δ�󶨣����� Inspector ��壡");
        }
    }

    private void Update()
    {
        if (player == null || !isInRegionA) return;

        // ��������Ƿ����ƶ�
        isMoving = Vector3.Distance(player.position, lastPosition) > 0.01f;
        lastPosition = player.position;

        // �������ǵ��ƶ�״̬����������Ч
        if (isMoving)
        {
            if (!regionAudio.isPlaying)
            {
                regionAudio.Play();
                Debug.Log("������Ч��ʼ���ţ������ƶ���");
            }
        }
        else
        {
            if (regionAudio.isPlaying)
            {
                regionAudio.Pause();
                Debug.Log("������Ч��ͣ���ţ�����δ�ƶ���");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Object entered trigger: {other.name}, Tag: {other.tag}");

        // ��������������� A
        if (other.gameObject == regionA.gameObject)
        {
            Debug.Log("���ǽ������� A");
            isInRegionA = true;

            // ��ͣ��������
            if (backgroundMusic != null && backgroundMusic.isPlaying)
            {
                backgroundMusic.Pause();
                Debug.Log("����������ͣ");
            }
            return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"Object exited trigger: {other.name}, Tag: {other.tag}");

        // ��������������� A
        if (other.gameObject == regionA.gameObject)
        {
            Debug.Log("�����뿪���� A");
            isInRegionA = false;

            // �ָ���������
            if (backgroundMusic != null && !backgroundMusic.isPlaying)
            {
                backgroundMusic.Play();
                Debug.Log("�������ָֻ�����");
            }

            // ֹͣ������Ч
            if (regionAudio.isPlaying)
            {
                regionAudio.Stop();
                Debug.Log("������Чֹͣ����");
            }
        }
    }

    // ��̬����������������
    public void SetBackgroundVolume(float volume)
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.volume = volume;
            Debug.Log("����������������Ϊ��" + volume);
        }
    }

    // ��̬����������Ч����
    public void SetRegionVolume(float volume)
    {
        if (regionAudio != null)
        {
            regionAudio.volume = volume;
            Debug.Log("������Ч��������Ϊ��" + volume);
        }
    }
}
