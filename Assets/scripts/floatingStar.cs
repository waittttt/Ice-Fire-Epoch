using UnityEngine;

public class FloatingStar : MonoBehaviour
{
    public float minY = 1.0f; // y轴的最小值
    public float maxY = 3.0f; // y轴的最大值
    public float speed = 1.0f; // 星星的移动速度

    private float targetY; // 星星的目标位置

    void Start()
    {
        // 初始化目标位置为随机值
        targetY = Random.Range(minY, maxY);
    }

    void Update()
    {
        // 获取当前星星的位置
        Vector3 currentPosition = transform.position;

        // 计算星星在y轴方向上的运动
        currentPosition.y = Mathf.MoveTowards(currentPosition.y, targetY, speed * Time.deltaTime);

        // 更新星星的位置
        transform.position = currentPosition;

        // 如果星星到达目标位置，则生成新的目标位置
        if (Mathf.Abs(currentPosition.y - targetY) < 0.01f)
        {
            targetY = Random.Range(minY, maxY);
        }
    }
}
