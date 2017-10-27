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

    public static float Time { get; private set; }
    public static int Score { get; private set; }

    void Start()
    {
        singleton = this;
        StartTimer();
    }

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

    #region Score
    public static void AddScore(int score)
    {
        Score += score;
        singleton.scoreText.text = "Score: " + Score;
    }
    #endregion
}
