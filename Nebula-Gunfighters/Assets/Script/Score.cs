using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // スコア
    private int score;
    private Text text;

    void Awake()
    {
        text = GetComponent<Text>();
        InitScore(); // スコア初期化の関数を呼び出し
    }

    // スコアを初期化する
    void InitScore()
    {
        this.score = 0;
        UpdateText();
    }

    // スコアを加算する
    void AddScore(int score)
    {
        this.score += score;
        UpdateText();
    }

    void UpdateText()
    {
        // Textを書き換える
        text.text = "Score:" + this.score;
    }
}
