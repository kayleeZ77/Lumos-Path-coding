using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionBManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource backgroundMusic;  // �������ֲ�����
    public AudioSource speakerAudio;     // ���ȵ���Ч������

    [Header("Region and Player")]
    public Collider regionB;             // ���� B �Ĵ�����
    public Transform player;             // ���ǵ� Transform

    [Header("Audio Settings")]
    public float targetVolume = 0.1f;    // �������ֽ���������Ŀ��������ԭ������ʮ��֮һ��
    public float volumeFadeSpeed = 1f;   // �����仯�ٶ�
    private float originalVolume;        // �������ֵ�ԭʼ����

    private bool isInRegionB = false;    // �����Ƿ������� B ��

    private void Start()
    {
        // ���汳�����ֵ�ԭʼ����
        if (backgroundMusic != null)
        {
            originalVolume = backgroundMusic.volume;
        }

        // ȷ��������Ч��������
        if (speakerAudio != null)
        {
            speakerAudio.spatialBlend = 1f; // ����Ϊ 3D ��Ч
            speakerAudio.loop = true;      // ѭ������
            speakerAudio.Stop();           // ��ʼ״̬������
        }

        // ����
        if (player == null)
        {
            Debug.LogError("Player Transform δ�󶨣����� Inspector ��壡");
        }

        if (regionB == null)
        {
            Debug.LogError("Region B δ�󶨣����� Inspector ��壡");
        }
    }

    private void Update()
    {
        // ƽ������������������
        if (backgroundMusic != null)
        {
            float target = isInRegionB ? originalVolume * targetVolume : originalVolume;
            backgroundMusic.volume = Mathf.Lerp(backgroundMusic.volume, target, Time.deltaTime * volumeFadeSpeed);
            Debug.Log($"�������ֵ�ǰ������{backgroundMusic.volume}");
        }

        // ����������Ч���������ݾ��룩
        if (isInRegionB && speakerAudio != null)
        {
            float distance = Vector3.Distance(player.position, speakerAudio.transform.position);
            float maxDistance = speakerAudio.maxDistance;
            speakerAudio.volume = Mathf.Clamp01(1f - (distance / maxDistance));
            Debug.Log($"������������Ϊ��{speakerAudio.volume}�����Ǿ��룺{distance}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered Region B");
            isInRegionB = true;

            // ����������Ч
            if (speakerAudio != null && !speakerAudio.isPlaying)
            {
                speakerAudio.Play();
                Debug.Log("������Ч��ʼ����");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited Region B");
            isInRegionB = false;

            // ֹͣ������Ч
            if (speakerAudio != null && speakerAudio.isPlaying)
            {
                speakerAudio.Stop();
                Debug.Log("������Чֹͣ����");
            }
        }
    }
}
