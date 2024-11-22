using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public float scoreMultiplier = 1f;
    public float timeLeft;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public int score;
    public int highScore;
    public bool isFinish;
    public HighScoreManager highScoreManager;

    private void Start() 
    {
        highScoreManager = GetComponent<HighScoreManager>();
    }

    private void Update() 
    {
        CheckHighScore();
    }

    public void UpdateScore(bool correct, float timeLeft)
    {
        // score = scoreData.score;

        if(correct == true){
            if(timeLeft < 2f){
                timeLeft = 2f;
            }
            
            score = Mathf.RoundToInt(score + (timeLeft/2 * 200f));
            scoreText.text = score.ToString();

            // scoreData.score = score;
        } else {
            score = Mathf.RoundToInt(score - 200f);
            scoreText.text = score.ToString();

            // scoreData.score = score;
        }
    }

    public void AddData()
    {
        highScoreManager.score = score;
    }

    public void CheckHighScore()
    {
        

        if(isFinish && SceneManager.GetActiveScene().name == "MainMenu" ){
            score = highScoreManager.score;
            highScore = highScoreManager.highScore;

            if(score > highScore){
                highScore = score;
                highScoreText.text = highScore.ToString();
            }

            isFinish = false;

            score = 0;
        }
    }
}
