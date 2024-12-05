using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAnimation : MonoBehaviour
{
    public GameObject circlePrefab; // Բ��Image��Ԥ����
    public int circleCount = 13;    // Բ������
    public float animationDuration = 1f; // ������ʱ��
    public float maxDistance = 200f; // Բ���ƶ�����������
    private List<RectTransform> circles = new List<RectTransform>(); // �洢����Բ��RectTransform
    private Vector2[] targetPositions; // Ŀ��λ������

    private float elapsedTime = 0f; // ��ǰʱ��
    private bool isMovingOutward = true; // �Ƿ������ƶ�

    void Start()
    {
        // ��ʼ��Ŀ��λ��
        targetPositions = new Vector2[circleCount];

        // ������Ĳ�����ƣ�����Ŀ��λ��
        CalculateTargetPositions();

        // ����Բ��
        for (int i = 0; i < circleCount; i++)
        {
            GameObject circle = Instantiate(circlePrefab, transform);
            RectTransform rectTransform = circle.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = Vector2.zero; // ��ʼ����Բ��
            circles.Add(rectTransform);
        }

        // ��ʼ����
        StartCoroutine(AnimateCircles());
    }

    void CalculateTargetPositions()
    {
        // ��ֱ�߲�������Ŀ��λ�ã�����������ͼ������
        targetPositions[0] = new Vector2(0, maxDistance);
        targetPositions[1] = new Vector2(0, -maxDistance);
        targetPositions[2] = new Vector2(maxDistance, 0);
        targetPositions[3] = new Vector2(-maxDistance, 0);
        targetPositions[4] = new Vector2(maxDistance * 0.7f, maxDistance * 0.7f);
        targetPositions[5] = new Vector2(-maxDistance * 0.7f, maxDistance * 0.7f);
        targetPositions[6] = new Vector2(maxDistance * 0.7f, -maxDistance * 0.7f);
        targetPositions[7] = new Vector2(-maxDistance * 0.7f, -maxDistance * 0.7f);
        targetPositions[8] = new Vector2(maxDistance * 0.4f, maxDistance * 0.4f);
        targetPositions[9] = new Vector2(-maxDistance * 0.4f, maxDistance * 0.4f);
        targetPositions[10] = new Vector2(maxDistance * 0.4f, -maxDistance * 0.4f);
        targetPositions[11] = new Vector2(-maxDistance * 0.4f, -maxDistance * 0.4f);
        targetPositions[12] = Vector2.zero; // ���ĵ�
    }

    IEnumerator AnimateCircles()
    {
        while (true)
        {
            elapsedTime = 0f;

            // ��������
            while (elapsedTime < animationDuration)
            {
                elapsedTime += Time.deltaTime;

                // ����ÿ��Բ��λ��
                for (int i = 0; i < circles.Count; i++)
                {
                    Vector2 start = isMovingOutward ? Vector2.zero : targetPositions[i];
                    Vector2 end = isMovingOutward ? targetPositions[i] : Vector2.zero;

                    // ��ֵ����λ��
                    circles[i].anchoredPosition = Vector2.Lerp(start, end, elapsedTime / animationDuration);
                }

                yield return null;
            }

            // ��ת����
            isMovingOutward = !isMovingOutward;
        }
    }
}
