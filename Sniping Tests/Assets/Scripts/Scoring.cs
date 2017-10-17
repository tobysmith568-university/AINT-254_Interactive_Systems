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

    public static float Time { get; private set; }
    public static int Score { get; private set; }

    void Start()
    {
        StartTimer();
    }

#region Time
    void StartTimer()
    {
        InvokeRepeating("Tick", 0.01f, 0.01f);
    }

    void StopTimer()
    {
        CancelInvoke("Tick");
    }

    void Tick()
    {
        Time = Time + 0.01f;
        timeText.text = Time.ToString("n2");
    }
#endregion

#region Score
    void  AddScore(int score)
    {
        Score += score;
    }
#endregion
}
