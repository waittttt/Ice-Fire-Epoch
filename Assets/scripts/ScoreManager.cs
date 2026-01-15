using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 引入 UI 命名空间

public class ScoreManager : MonoBehaviour
{
    public Text scoreText; // UI 文本，用于显示分数
    private int score = 0; // 分数
    void Start()
    {
        
    }

    public void AddScore(int value)
    {
        score += value; // 增加分数
        UpdateScoreText(); // 更新 UI
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score:                          " + score+"/3"; // 更新显示文本
        }
    }
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
