using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    protected Animator anim;
    public float moveDistance = 0.1f;
    public float moveSpeed = 5f;  // 人物的移动速度
    public float jumpHeight = 10f; // 跳跃的最大高度
    public float jumpForce = 10f;  // 跳跃力，控制向上的速度

    private Vector3 currentPosition;
    private float currentHeight = 0;
    private Vector3 moveDirection = Vector3.zero;  // 存储人物的移动方向
    private bool isRight;

    private float initialXPosition;
    private Coroutine jumpCoroutine;

    public bool isGrounded = false;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Rigidbody rb;  // 使用 Rigidbody 来处理物理运动

    void Start()
    {
        initialXPosition = transform.position.x;
        anim = GetComponent<Animator>();
        isRight = true;
        currentPosition = transform.position;
        currentHeight = currentPosition.y;

        rb = GetComponent<Rigidbody>();  // 获取 Rigidbody 组件
    }

    void Update()
    {
        // 检查是否在地面
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (!isGrounded)
            anim.SetBool("fall", true);
        else
            anim.SetBool("fall", false);

        if (anim)
        {
            transform.position = new Vector3(initialXPosition, transform.position.y, transform.position.z);
            currentPosition = transform.position;
            currentHeight = currentPosition.y;

            // 处理水平移动
            float h = Input.GetAxis("Horizontal");

            // 角色方向控制
            if (isRight && h < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                isRight = false;
            }
            else if (!isRight && h > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                isRight = true;
            }

            anim.SetBool("isRight", isRight);

            // 水平移动方向
            if (h == 0)
                moveDirection = Vector3.zero;
            else
                moveDirection = Vector3.forward;

            // 处理水平移动，直接控制 Rigidbody 的速度
            Vector3 velocity = rb.velocity;
            velocity.x = moveDirection.x * moveSpeed; // 保持水平速度
            velocity.z = moveDirection.z * moveSpeed;
            rb.velocity = velocity; // 更新 Rigidbody 速度

            anim.SetFloat("speed", h);

            // 跳跃操作
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                anim.SetBool("jump", true);

                // 施加跳跃力
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z); // 施加向上的速度

                // 启动跳跃协程，用来处理跳跃的平滑过渡
                // 这里我们不再需要协程来控制跳跃高度，因为通过 Rigidbody 的 velocity 控制垂直速度即可
            }
            else
            {
                anim.SetBool("jump", false);
            }
        }
    }
}
