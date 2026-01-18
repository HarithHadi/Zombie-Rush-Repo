using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject hud;
    public TMP_Text scoreText;
    public TMP_Text HighscoreText;

    public void Setup(int score, int highscore) 
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
        hud.SetActive(false);
        scoreText.text = score.ToString();
        HighscoreText.text = highscore.ToString();
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Default Scene");
        hud.SetActive(true);
        Time.timeScale = 1f;
    }
    public void ResumeButton()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        hud.SetActive(true);
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
