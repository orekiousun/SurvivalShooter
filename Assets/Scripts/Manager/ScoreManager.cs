using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
///
/// <summary>
public class ScoreManager : MonoBehaviour
{

    public Text scoreText;
    // 设置为静态变量
    public static int score;

    void Start()
    {
        scoreText = this.GetComponent<Text>();
    }
    
    void Update()
    {
        scoreText.text = "Score: " + score;
    }
}
