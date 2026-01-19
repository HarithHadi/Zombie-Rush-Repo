using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Leaderboards.Models;
using UnityEngine;

[System.Serializable]
public class LeaderBoardEntry : IComparable<LeaderBoardEntry>
{
    public string playerName;
    public int score;

    public LeaderBoardEntry(string name, int score)
    {
        this.playerName = name;
        this.score = score;
    }

    public int CompareTo(LeaderBoardEntry other)
    {
        // Sort in descending order (highest score first)
        return other.score.CompareTo(this.score);
    }
}
