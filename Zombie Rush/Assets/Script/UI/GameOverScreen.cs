using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameOverScreen : MonoBehaviour
{
    public GameObject hud;
    public TMP_Text scoreText;
    public TMP_Text HighScoreText;
    public TMP_InputField nameInput;

    [Header("Leaderboard")]
    public GameObject nameInputPanel; // Panel containing the name input
    public GameObject leaderboardPanel; // Panel showing the leaderboard
    public GameObject retryButton; // Your retry button
    public GameObject exitButton; // Your exit button
    public GameObject submitButton; // Your exit button

    private int currentScore;
    
    public void Setup(int score) 
    {
        currentScore = score;
        gameObject.SetActive(true);
        hud.SetActive(false);
        scoreText.text = score.ToString();

        //HighScore
        HighScoreManager.instance.SaveHighScore(score);
        int highScore = HighScoreManager.instance.getHighScore();
        HighScoreText.text = highScore.ToString();

        if (HighScoreManager.instance.IsScoreInTopTen(score))
        {
            ShowNameInput();
        }
        else
        {
            ShowLeaderboard();
        }
    }

    public void SubmitScore(int score) 
    {
        string playerName = nameInput.text;

        if (string.IsNullOrEmpty(playerName))
            playerName = "Some Dude";

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

    private void ShowNameInput()
    {
        // Show name input, hide leaderboard and buttons
        nameInputPanel.SetActive(true);
        leaderboardPanel.SetActive(false);
        retryButton.SetActive(false);
        exitButton.SetActive(false);

        nameInput.text = "";
        nameInput.Select();
        nameInput.ActivateInputField();
    }

    private void ShowLeaderboard()
    {
        // Hide name input, show leaderboard and buttons
        nameInputPanel.SetActive(false);
        leaderboardPanel.SetActive(true);
        retryButton.SetActive(true);
        exitButton.SetActive(true);
        submitButton.SetActive(false);

        // Update leaderboard display
        LeaderBoardUI leaderboardUI = leaderboardPanel.GetComponent<LeaderBoardUI>();
        if (leaderboardUI != null)
        {
            leaderboardUI.UpdateLeaderboardDisplay();
        }
    }

    public void SubmitScore()
    {
        string playerName = nameInput.text.Trim();
        if (string.IsNullOrEmpty(playerName))
            playerName = "Some Dude";
        
        HighScoreManager.instance.AddLeaderboardEntry(playerName, currentScore);

        ShowLeaderboard();
    }


}
