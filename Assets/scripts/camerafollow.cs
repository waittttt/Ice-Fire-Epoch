using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour
{
    // 要跟随的目标
    public Transform target;

    // 摄像机与目标在Y轴和Z轴上的相对偏移量
    public Vector3 offset = new Vector3(-2f, 0.6f, -2f);

    // 每秒跟随速度（可选，用于平滑跟随）
    public float followSpeed = 5.0f;

    // Z轴上的限制范围
    public float minZ = -10f; // 最小Z值
    public float maxZ = 10f;  // 最大Z值

    // Y轴上的固定值
    public List<float> fixedYPositions = new List<float> { -3.4f, -1.15f, 1.15f,3.4f }; // 摄像机允许的固定Y值
    public float ySwitchThreshold = 1f; // 切换阈值范围
    public float smoothYTransitionSpeed = 2f; // Y轴切换时的平滑速度

    private float targetY; // 摄像机当前目标的Y轴位置

    void Start()
    {
        // 初始化摄像机的Y轴目标位置为当前位置
        targetY = transform.position.y;
    }

    void Update()
    {
        if (target != null)
        {
            // 计算摄像机的目标位置
            Vector3 desiredPosition = target.position + offset;

            // 限制摄像机的Z轴位置
            desiredPosition.z = Mathf.Clamp(desiredPosition.z, minZ, maxZ);

            // 获取目标的最近固定Y值
            float closestY = GetClosestY(target.position.y);

            // 平滑过渡到目标Y值
            targetY = Mathf.Lerp(targetY, closestY, Time.deltaTime * smoothYTransitionSpeed);
            desiredPosition.y = targetY;

            // 平滑移动摄像机
            Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * followSpeed);
            transform.position = smoothPosition;
        }
    }

    // 获取最接近的固定Y值
    private float GetClosestY(float targetY)
    {
        float closestY = transform.position.y;

        foreach (float fixedY in fixedYPositions)
        {
            if (Mathf.Abs(targetY - fixedY) <= ySwitchThreshold)
            {
                closestY = fixedY;
                break;
            }
        }

        return closestY;
    }
}
