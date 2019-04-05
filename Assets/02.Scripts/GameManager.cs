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
    public GameObject gameOver;
    public PlayerCtroll playerControl;
    public EnemyFire enemyFire;

    private void Awake()
    {
        instance = this;
        UpdateUI();
        Reset();
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
    public void PlayerDie(bool check)
    {
        if (check)
        {
            gameOver.SetActive(true);
            enemyFire.enabled = false;
            playerControl.enabled = false;

        }
    }
    public void GameClear(bool check)
    {
        if (check)
        {
            enemyFire.enabled = false;
            playerControl.enabled = false;
        }
    }
    private void Reset()
    {
        score = 0;
        gameOver.SetActive(false);
        enemyFire.enabled = true;
        playerControl.enabled = true;
    }
}
