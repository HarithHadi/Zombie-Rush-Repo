using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Leaderboards.Models;
using UnityEngine;

public class LeaderBoardUI : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text leaderboardText;


    private void Start()
    {
        
    }

    public void UpdateLeaderboardDisplay() 
    {
        List<LeaderBoardEntry> leaderboard = HighScoreManager.instance.GetLeaderboard();

        // Build the leaderboard text
        string leaderboardDisplay = "";

        for (int i = 0; i < 10; i++)
        {
            if (i < leaderboard.Count)
            {
                // Entry exists - show data
                leaderboardDisplay += (i + 1) + ". " + leaderboard[i].playerName + " - " + leaderboard[i].score + "\n";
            }
            else
            {
                // No entry - show empty slot
                leaderboardDisplay += (i + 1) + ". --- - 0\n";
            }
        }

        // Update the text
        leaderboardText.text = leaderboardDisplay;
    }

}
