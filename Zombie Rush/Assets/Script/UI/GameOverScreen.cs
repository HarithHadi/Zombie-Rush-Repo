using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject hud;
    public TMP_Text scoreText;
    public TMP_Text HighScoreText;
    
    public void Setup(int score) 
    {
        gameObject.SetActive(true);
        hud.SetActive(false);
        scoreText.text = score.ToString();

        //HighScore
        int highScore = HighScoreManager.instance.getHighScore();
        HighScoreText.text = highScore.ToString();
    }

    public void RestartButton() 
    {
        SceneManager.LoadScene("Default Scene");
        hud.SetActive(true);
        Time.timeScale = 1f;

        
    }

    public void ExitButton() 
    {
        SceneManager.LoadScene("SampleScene");
    }


}
