using System.Collections;
using UnityEngine;

public class CharacterDeath : MonoBehaviour
{
    public string hazardTag; // 危险区域的标签（比如"Water"或"Fire"）

    public GameObject deathEffectPrefab; // 死亡特效预制体
    //public AudioClip deathSound;
    private Animator anim;

    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        anim.SetBool("death",false);
    }

    void OnTriggerEnter(Collider other)
    {
        // 检查碰撞对象的标签是否是危险区域
        if (other.CompareTag(hazardTag))
        {
            HandleDeath();
            FindObjectOfType<GameManager>().GameOver(); // 并行调用死亡界面显示方法
        }
        if (other.CompareTag("Door"))
        {
            HandleWin();
            FindObjectOfType<GameManager>().GameWin(); // 并行调用死亡界面显示方法
        }
    }

    private void HandleDeath()
    {
        /*
        if(deathSound!=null){
            audioSource.clip = deathSound;
            audioSource.loop = false; // 确保走路音效循环
            audioSource.Play();
            Debug.Log("Death Yinxiao");
        }*/
        // 播放死亡特效
        if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }

        // 禁用当前角色（模拟死亡效果）
        gameObject.SetActive(false);
        //StartCoroutine(realDeath());
        
        anim.SetBool("death",true);
    }/*
    private IEnumerator realDeath(){
        yield return new WaitForSeconds(audioSource.clip.length);
        gameObject.SetActive(false);
    }*/
    private void HandleWin()
    {
        // 禁用当前角色（模拟死亡效果）
        gameObject.SetActive(false);
    }
}
