using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;

    public Text scoreText;
    public Text bestScoreText;

    private void Awake()
    {
        instance = this;
        UpdateUI();

    }
    public void AddScore(int newScore)
    {
        score += newScore;
        UpdateUI();
        UpdateBestScore();
    }
    private void UpdateUI()
    {
        scoreText.text = "Score : " + score;
        bestScoreText.text = "BestScore : " + GetBestScore();
    }
    private void UpdateBestScore()
    {
        if (GetBestScore() < score)
        {
            PlayerPrefs.SetInt("BestScore", score);
        }
    }
    private int GetBestScore()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore");
        return bestScore;
    }

}
