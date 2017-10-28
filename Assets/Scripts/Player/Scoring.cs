using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Timers;

public class Scoring : MonoBehaviour
{
    /// <summary>
    /// THIS SCRIPT IS ADDED TO THE EVENT LISTENER GAMEOBJECT IN THE SCENE!
    /// </summary>
            
    [SerializeField]
    Text timeText;

    [SerializeField]
    Text scoreText;
    static Scoring singleton;

    [SerializeField]
    Transform targets;
    
    //The duration of the current level
    public static float Time { get; private set; }

    //The players current socre
    public static int Score { get; private set; }

    void Start()
    {
        singleton = this;
        StartTimer();
    }

    /// <summary>
    /// Code to change the game time
    /// </summary>
    #region Time
    static void StartTimer()
    {
        singleton.InvokeRepeating("Tick", 0.01f, 0.01f);
    }

    static void StopTimer()
    {
        singleton.CancelInvoke("Tick");
    }

    void Tick()
    {
        Time = Time + 0.01f;
        timeText.text = "Time: " + Time.ToString("n2");
    }
    #endregion

    /// <summary>
    /// Code to change the player's score
    /// </summary>
    #region Score
    public static void AddScore(int score)
    {
        Score += score;
        singleton.scoreText.text = "Score: " + Score;
    }
    #endregion
}
