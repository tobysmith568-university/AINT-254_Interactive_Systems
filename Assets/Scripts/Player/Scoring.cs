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
    public static int Time { get; private set; }

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
        singleton.InvokeRepeating("Tick", 1f, 1f);
    }

    static void StopTimer()
    {
        singleton.CancelInvoke("Tick");
    }

    void Tick()
    {
        Time = Time + 1;
        timeText.text = "Time: " + Time.ToString();
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
