using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAnimation : MonoBehaviour
{
    public GameObject circlePrefab; // 圆形Image的预制体
    public int circleCount = 13;    // 圆形数量
    public float animationDuration = 1f; // 动画总时长
    public float maxDistance = 200f; // 圆点移动到的最大距离
    private List<RectTransform> circles = new List<RectTransform>(); // 存储所有圆的RectTransform
    private Vector2[] targetPositions; // 目标位置数组

    private float elapsedTime = 0f; // 当前时间
    private bool isMovingOutward = true; // 是否向外移动

    void Start()
    {
        // 初始化目标位置
        targetPositions = new Vector2[circleCount];

        // 根据你的布局设计，设置目标位置
        CalculateTargetPositions();

        // 创建圆点
        for (int i = 0; i < circleCount; i++)
        {
            GameObject circle = Instantiate(circlePrefab, transform);
            RectTransform rectTransform = circle.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = Vector2.zero; // 初始化到圆心
            circles.Add(rectTransform);
        }

        // 开始动画
        StartCoroutine(AnimateCircles());
    }

    void CalculateTargetPositions()
    {
        // 按直线布局生成目标位置，这里根据你的图来调整
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
        targetPositions[12] = Vector2.zero; // 中心点
    }

    IEnumerator AnimateCircles()
    {
        while (true)
        {
            elapsedTime = 0f;

            // 动画过程
            while (elapsedTime < animationDuration)
            {
                elapsedTime += Time.deltaTime;

                // 计算每个圆的位置
                for (int i = 0; i < circles.Count; i++)
                {
                    Vector2 start = isMovingOutward ? Vector2.zero : targetPositions[i];
                    Vector2 end = isMovingOutward ? targetPositions[i] : Vector2.zero;

                    // 插值计算位置
                    circles[i].anchoredPosition = Vector2.Lerp(start, end, elapsedTime / animationDuration);
                }

                yield return null;
            }

            // 反转方向
            isMovingOutward = !isMovingOutward;
        }
    }
}
