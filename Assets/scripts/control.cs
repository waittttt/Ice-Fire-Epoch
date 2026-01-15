using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    protected Animator anim;
    public float moveDistance = 0.1f;
    public float moveSpeed = 5f;  // 人物的移动速度
    //public float jumpHeight = 10f;

    public float jumpSpeed = 5f;

    public float jumpForce = 10f;

    private bool isJumping = false;

    private float jumpVelocity = 0f; // 垂直速度

    private Vector3 currentPosition;
    private float currentHeight = 0;
    private Vector3 moveDirection = Vector3.zero;  // 存储人物的移动方向
    private bool isRight;

    private float initialXPosition;

    public bool isGrounded = false;

    public bool isBlocked = false;
    public Transform frontCheck;  // 用于检测前方障碍物的位置
    public float frontCheckDistance = 0.5f; // 检测前方障碍物的距离
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isPushing = false;

    private Rigidbody rb;

    public ScoreManager scoreManager;

    // 推动箱子的力大小
    public float pushForce = 10f;

    void Start()
    {
        initialXPosition = transform.position.x;
        anim = GetComponent<Animator>();
        isRight = true;
        currentPosition = transform.position;
        currentHeight = currentPosition.y;
        rb = GetComponent<Rigidbody>();

    }

    // 添加推动箱子的逻辑



    void Update()
    {
        //Debug.Log(groundCheck);
        //Debug.Log("GroundCheck Position: " + groundCheck.position);
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);


        //isBlocked = Physics.Raycast(frontCheck.position, Vector3.forward, frontCheckDistance, groundMask) || Physics.Raycast(frontCheck.position, Vector3.back, frontCheckDistance, groundMask);

        if (isPushing)
            anim.SetBool("push", true);
        else
            anim.SetBool("push", false);


        if (!isGrounded)
            anim.SetBool("fall", true);
        else
            anim.SetBool("fall", false);

        if (anim)
        {
            transform.position = new Vector3(initialXPosition, transform.position.y, transform.position.z);
            currentPosition = transform.position;
            currentHeight = currentPosition.y;
            float h = Input.GetAxis("Horizontal");

            if (isRight == true && h < 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                isRight = false;
            }
            if (isRight == false && h > 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                isRight = true;
            }
            anim.SetBool("isRight", isRight);

            if (h == 0)
                moveDirection = Vector3.zero;
            else
                moveDirection = Vector3.forward;

            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            anim.SetFloat("speed", h);

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                anim.SetBool("jump", true);
                //float targetHeight = jumpHeight + currentHeight;
                isJumping = true;
                jumpVelocity = jumpSpeed;
            }
            else
            {
                anim.SetBool("jump", false);
            }

            if (isJumping)
            {
                // 应用跳跃逻辑
                transform.Translate(Vector3.up * jumpVelocity * Time.deltaTime);
                jumpVelocity -= 9.8f * Time.deltaTime; // 模拟重力效果
            }
            if (isGrounded && jumpVelocity < 0)
                isJumping = false;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pushable")) // 确保箱子被标记为 "Pushable"
        {
            Rigidbody boxRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (boxRigidbody != null)
            {
                // 推动方向：根据角色朝向
                Vector3 pushDirection = transform.forward;

                // 添加力到箱子上
                boxRigidbody.AddForce(pushDirection * pushForce, ForceMode.Force);
                isPushing = true;

            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pushable"))
        {
            anim.SetBool("push", false); // 离开箱子时关闭推箱动画
            isPushing = false;
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Star")){
            if(scoreManager != null){
                scoreManager.AddScore(1);
            }
            Destroy(other.gameObject);

            Debug.Log("Star collected!");
        }
    }
}