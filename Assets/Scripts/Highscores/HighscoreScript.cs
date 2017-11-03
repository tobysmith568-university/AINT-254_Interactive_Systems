using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// THIS SCRIPT IS ADDED TO THE EVENT LISTENER GAMEOBJECT IN THE SCENE!
/// </summary>
public class HighscoreScript : MonoBehaviour
{
    [SerializeField]
    Text scoresText;
    [SerializeField]
    Text timesText;

    void Start()
    {
        //Show the score highscores
        string message = "The Score Highscores:";
        foreach (GameScore score in MyPrefs.HighScores)
        {
            message = message + "\n" + score.Name + ": " + score.Score;
        }
        scoresText.text = message;
        
        //Show the time highscores
        message = "This Time Lowscores:";
        foreach (GameScore score in MyPrefs.LowTimes)
        {
            message = message + "\n" + score.Name + ": " + new System.DateTime().Add(score.Time).ToString("m:ss:f");
        }
        timesText.text = message;
    }

    /// <summary>
    /// Called by the MainMenu button to load the main menu scene
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
