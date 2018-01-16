using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// THIS SCRIPT IS ADDED TO THE EVENT LISTENER GAMEOBJECT IN THE SCENE!
/// </summary>
public class Scoring : MonoBehaviour
{            
    [SerializeField]
    Text timeText;

    [SerializeField]
    Text scoreText;

    static Scoring singleton;

    [SerializeField]
    Transform targets;
    
    //The duration of the current level
    public static float Time { get; private set; }

    //The players current score
    public static int Score { get; private set; }

    void Start()
    {
        singleton = this;
        StartTimer();
    }

    /// <summary>
    /// Resets the Time and Score to zero
    /// </summary>
    public static void FullReset()
    {
        Time = 0f;
        Score = 0;
    }

    #region Time

    /// <summary>
    /// Starts a timer which ticks every second
    /// </summary>
    static void StartTimer()
    {
        singleton.InvokeRepeating("Tick", 0.1f, 0.1f);
    }

    /// <summary>
    /// Stops the timer which ticks every second
    /// </summary>
    static void StopTimer()
    {
        singleton.CancelInvoke("Tick");
    }

    /// <summary>
    /// The tick of the timer which ticks every second
    /// </summary>
    void Tick()
    {
        Time = Time + 0.1f;
        timeText.text = Time.ToString("0.0");
    }
    #endregion

    #region Score

    /// <summary>
    /// Adds a given value to the player score
    /// </summary>
    public static void AddScore(int score)
    {
        Score += score;
        singleton.scoreText.text = "" + Score;
    }

    #endregion
}
