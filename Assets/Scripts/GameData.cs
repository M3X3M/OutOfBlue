using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static bool Is_Ship2{ get; set; }
    private static int Score = 0;

    public static int getScore()
    {
        return Score;
    }

    public static void AddScore(int amount)
    {
        Score = Score + amount;
    }

    public static void ResetScore()
    {
        Score = 0;
    }
}