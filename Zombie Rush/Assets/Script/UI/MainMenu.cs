using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject chooseDifficulty;
    public LeaderBoardUI leaderboardUI;
    void Start()
    {
        if (leaderboardUI != null) 
        {
            leaderboardUI.UpdateLeaderboardDisplay();
        }
        
        AudioManager.Instance.ChangeMusic(AudioManager.SoundType.Music_Menu);
        Time.timeScale = 1f;
        chooseDifficulty.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton() 
    {
        chooseDifficulty.SetActive(true);
        gameObject.SetActive(false);
        
    }

    public void QuitButton() 
    {
        Application.Quit();
    }

    public void EasyMode() 
    {
        GameSettings.Difficulty = Difficulty.Easy;
        SceneManager.LoadScene("Default Scene");
        Time.timeScale = 1.0f;
        

    }
    public void HardMode()
    {
        GameSettings.Difficulty = Difficulty.Hard;
        SceneManager.LoadScene("Default Scene");
        Time.timeScale = 1.0f;
    }
}
