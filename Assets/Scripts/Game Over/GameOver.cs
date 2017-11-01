using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    Text time;
    [SerializeField]
    Text score;
    void Start()
    {
        
        time.text = "Time:\n" + new System.DateTime().Add(System.TimeSpan.FromSeconds(Scoring.Time)).ToString("m:ss");
        score.text = "Score:\n" + Scoring.Score.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
