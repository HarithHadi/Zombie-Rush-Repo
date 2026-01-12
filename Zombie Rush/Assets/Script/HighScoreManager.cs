using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager instance;

    private const string highScoreKey = "HighScore";
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
        
    }

    public int getHighScore() 
    {
        return PlayerPrefs.GetInt(highScoreKey, 0);
    }

    public void SaveHighScore(int score) 
    {
        int currentHigh = getHighScore();

        if (score > currentHigh) 
        {
            PlayerPrefs.SetInt(highScoreKey, score);
            PlayerPrefs.Save();
        }
        Debug.Log("Saved high score: " + PlayerPrefs.GetInt("HighScore"));
    }
}
