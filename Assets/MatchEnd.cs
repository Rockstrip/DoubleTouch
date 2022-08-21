using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class MatchEnd : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreboard;
    public enum Result
    {
        Victory,
        Defeat
    }
    public void EndMatch(Result result)
    {
        scoreboard.text = result switch
        {
            Result.Victory => "Victory",
            Result.Defeat  => "Defeat",
            _              => throw new ArgumentOutOfRangeException(nameof(result), result, null)
        };
    }
}
