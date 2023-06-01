using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameRunning = true;

    public int score = 0;

    public GameObject gameOverScreen, player, tail;

    public TMP_Text scoreText, finalScoreText, highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);
        UpdateScore(0);
    }

    public void EndGame()
    {
        tail.GetComponent<TailScript>().audio.Play();
        gameOverScreen.SetActive(true);
        finalScoreText.text = "Your Score: " + score;

        int originalHighScore = PlayerPrefs.GetInt("HIGH_SCORE");
        if (score > originalHighScore)
        {
            highScoreText.text = "New High Score!";
            PlayerPrefs.SetInt("HIGH_SCORE", score);
        }
        else
            highScoreText.text = "High Score: " + originalHighScore;

        Debug.Log("Game over");
        gameRunning = false;
    }

    public void UpdateScore(int scoreChange)
    {
        score += scoreChange;
        scoreText.text = "Score: " + score;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
