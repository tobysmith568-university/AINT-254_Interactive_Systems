using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Timers;

public class Scoring : MonoBehaviour
{
    [SerializeField]
    Text timeText;

    public static float Time { get; private set; }
    public static int Score { get; private set; }

    private void Start()
    {
        StartTimer();
    }

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
}
