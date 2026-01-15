using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public GameObject effectPrefab; // 特效预制体
    public int scoreValue = 1; // 每颗星星的分值
    

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 确保碰撞对象是玩家
        {
            // 触发特效
            
            PlayEffect();
            StarDisplayManager starDisplay=FindObjectOfType<StarDisplayManager>();
            if(starDisplay!=null){
                starDisplay.AddStar();
            }

            // 增加分数
            //GameManager.instance.AddScore(scoreValue);

            // 销毁星星
            Destroy(gameObject);
        }
    }

    private void PlayEffect()
    {
        if (effectPrefab != null)
        {
            // 在星星的位置生成特效
            Instantiate(effectPrefab, transform.position, Quaternion.identity);

            // 销毁特效以避免资源占用（假设特效持续时间是2秒）
            Destroy(effectPrefab, 2f);
        }
    }
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
