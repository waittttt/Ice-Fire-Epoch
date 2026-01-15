using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip jumpSound; // 跳跃音效
    public AudioClip walkSound; // 走路音效
    //public AudioClip deathSound;
    private Animator anim;
    private bool isJumping = false; // 用于检测跳跃状态

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 控制走路音效
        if (anim.GetFloat("speed") != 0f)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = walkSound;
                audioSource.loop = true; // 确保走路音效循环
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying && audioSource.clip == walkSound)
            {
                audioSource.Stop();
            }
        }

        // 控制跳跃音效
        if (anim.GetBool("jump") && !isJumping)
        {
            isJumping = true;
            audioSource.clip = jumpSound;
            audioSource.loop = false; // 确保走路音效循环
            audioSource.Play();
        }

        // 检测跳跃结束（根据需要调整逻辑）
        if (!anim.GetBool("jump") && isJumping)
        {
            isJumping = false;
        }

        
    }
}
