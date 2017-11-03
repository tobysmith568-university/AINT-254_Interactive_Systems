using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// THIS SCRIPT IS ADDED TO THE EVENT LISTENER GAMEOBJECT IN THE SCENE!
/// </summary>
public class GameOver : MonoBehaviour
{
    [SerializeField]
    Text time;
    [SerializeField]
    Text score;
    [SerializeField]
    InputField playerName;

    GameScore[] timeScores;
    GameScore[] scoreScores;
    GameScore gameScore;

    void Start()
    {
        //Get the PlayerPrefs
        timeScores = MyPrefs.LowTimes;
        scoreScores = MyPrefs.HighScores;
        gameScore = MyPrefs.LastPlay;

        //Show the stats
        time.text = "Time:\n" + new System.DateTime().Add(gameScore.Time).ToString("m:ss:f");
        score.text = "Score:\n" + gameScore.Score.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    /// <summary>
    /// Called by the MainMenu button to load the main menu scene
    /// </summary>
    public void MainMenu()
    {
        //Set the PlayerPrefs
        gameScore = new GameScore(playerName.text, MyPrefs.LastPlay.Score, MyPrefs.LastPlay.Time);
        
        //Rank the time in the time highscores
        for (int i = 0; i < timeScores.Length; i++)
        {
            if (gameScore.FasterThan(timeScores[i]))
            {
                for (int ii = 9; ii > i; ii--)
                {
                    timeScores[ii] = timeScores[ii - 1];
                }
                timeScores[i] = gameScore;
                break;
            }
        }

        //Rank the score in the score highscores
        for (int i = 0; i < scoreScores.Length; i++)
        {
            if (gameScore.ScoredMoreThan(scoreScores[i]))
            {
                for (int ii = 9; ii > i; ii--)
                {
                    scoreScores[ii] = scoreScores[ii - 1];
                }
                scoreScores[i] = gameScore;
                break;
            }
        }

        //Save the new highscores
        MyPrefs.LowTimes = timeScores;
        MyPrefs.HighScores = scoreScores;
        
        //Switch the scene
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
