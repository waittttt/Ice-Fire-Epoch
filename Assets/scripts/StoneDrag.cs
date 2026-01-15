using UnityEngine;

public class StoneDrag : MonoBehaviour
{
    public AudioClip dragSound; // 拖动音效
    private AudioSource audioSource; // 播放音效的组件
    private Rigidbody rb; // 石头的刚体
    private bool isDragging = false; // 标记是否正在拖动

    void Start()
    {
        // 获取必要的组件
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        // 设置 AudioSource 的属性
        if (audioSource != null)
        {
            audioSource.clip = dragSound;
            audioSource.loop = true; // 循环播放拖动音效
            audioSource.playOnAwake = false; // 开始时不播放
        }
    }

    void Update()
    {
        // 检查石头是否正在移动
        if (rb.velocity.magnitude > 0.01f) // 速度阈值，避免静止时播放声音
        {
            if (!isDragging)
            {
                // 开始播放拖动音效
                isDragging = true;
                audioSource.Play();
            }
        }
        else
        {
            if (isDragging)
            {
                // 停止播放拖动音效
                isDragging = false;
                audioSource.Stop();
            }
        }
    }
}
