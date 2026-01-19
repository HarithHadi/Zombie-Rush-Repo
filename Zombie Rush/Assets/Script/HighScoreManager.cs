using System.Collections;
using System.Collections.Generic;
using Unity.Services.Leaderboards.Models;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager instance;

    private const string highScoreKey = "HighScore";

    private const string leaderboardKey = "LeaderboardData";
    private const int maxLeaderboardEntries = 10;
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

    public bool IsScoreInTopTen(int score) 
    {
        List<LeaderBoardEntry> leaderboard = GetLeaderboard();

        if (leaderboard.Count < maxLeaderboardEntries)
            return true;

        return score > leaderboard[leaderboard.Count - 1].score;
    }
    
    
    public void AddLeaderboardEntry(string playerName, int score)
    {
        List<LeaderBoardEntry> leaderboard = GetLeaderboard();

        leaderboard.Add(new LeaderBoardEntry(playerName, score));
        leaderboard.Sort();

        // Keep only top 10
        if (leaderboard.Count > maxLeaderboardEntries)
        {
            leaderboard.RemoveRange(maxLeaderboardEntries, leaderboard.Count - maxLeaderboardEntries);
        }

        SaveLeaderboard(leaderboard);
    }

    public List<LeaderBoardEntry> GetLeaderboard()
    {
        string json = PlayerPrefs.GetString(leaderboardKey, "");

        if (string.IsNullOrEmpty(json))
        {
            return new List<LeaderBoardEntry>();
        }

        LeaderBoardData data = JsonUtility.FromJson<LeaderBoardData>(json);
        return data.entries;
    }

    private void SaveLeaderboard(List<LeaderBoardEntry> leaderboard)
    {
        LeaderBoardData data = new LeaderBoardData();
        data.entries = leaderboard;

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(leaderboardKey, json);
        PlayerPrefs.Save();
    }

    public void ClearLeaderboard()
    {
        PlayerPrefs.DeleteKey(leaderboardKey);
        PlayerPrefs.Save();
    }
}
