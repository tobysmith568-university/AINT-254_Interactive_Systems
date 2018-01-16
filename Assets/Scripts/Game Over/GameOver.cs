using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net;

/// <summary>
/// THIS SCRIPT IS ADDED TO THE EVENT LISTENER GAMEOBJECT IN THE SCENE!
/// </summary>
public class GameOver : MonoBehaviour
{
    [SerializeField]
    Image title;
    [SerializeField]
    Button menuButton;

    [SerializeField]
    Text time;
    [SerializeField]
    Text score;
    [SerializeField]
    InputField playerName;

    [SerializeField]
    AudioSource whoop;

    GameScore[] timeScores;
    GameScore[] scoreScores;
    GameScore gameScore;

    int index = 0;

    void Start()
    {
        TitleExit();

        //Get the PlayerPrefs
        timeScores = MyPrefs.LowTimes;
        scoreScores = MyPrefs.HighScores;
        gameScore = MyPrefs.LastPlay;

        //Show the stats
        time.text = new System.DateTime().AddMilliseconds(gameScore.Duration).ToString("m:ss:f");
        score.text = gameScore.Score.ToString();
        playerName.text = gameScore.Name;
    }

    void Update()
    {
        //If there's text in the name text box, allow the button to be clicked
        menuButton.interactable = playerName.text.Length > 0;

        //If the button can be pressed and 'Return' is pressed
        if (Input.GetKeyDown(KeyCode.Return) && menuButton.interactable)
            MainMenu();
    }

    /// <summary>
    /// Called by the MainMenu button to load the main menu scene
    /// </summary>
    public void MainMenu()
    {
        //Set the PlayerPrefs
        MyPrefs.LastPlay = gameScore = new GameScore(playerName.text == ""? "No name" : playerName.text, MyPrefs.LastPlay.Score, MyPrefs.LastPlay.Duration);
        
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
        
        //Send the highscore off to the server
        using (WebClient wc = new WebClient())
        {
            string url = "http://tobysmith.uk/ShooterUnknown/SendScore.php?" +
                "pass=we345678i9olkjhgtrewazsxcvbnjkio90pokjyt432waw23erfr567ujhg" +
                "&score={\"Name\":\"" + gameScore.Name + "\",\"Duration\":" + gameScore.Duration + ",\"Score\":" + gameScore.Score + "}";
            wc.DownloadStringAsync(new System.Uri(url));
        }

        //Save the new highscores
        MyPrefs.LowTimes = timeScores;
        MyPrefs.HighScores = scoreScores;
        
        //Switch the scene
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    /// <summary>
    /// Changes the colour of the title when rolled-over
    /// </summary>
    public void TitleEnter()
    {
        Color color;
        string[] colours = { "#FFB32AFF", "#33D311FF", "#008EE3FF", "#A216D4FF" };
        ColorUtility.TryParseHtmlString(colours[index], out color);
        title.color = color;
        index = (index == colours.Length - 1) ? 0 : index + 1;

        Whoop();
    }

    /// <summary>
    /// Returns the colour of the title to it's default
    /// </summary>
    public void TitleExit()
    {
        Color color;
        ColorUtility.TryParseHtmlString("#AD4043FF", out color);
        title.color = color;
    }

    /// <summary>
    /// Plays the roll-over 'Whoop' sound
    /// </summary>
    public void Whoop()
    {
        if (!whoop.isPlaying)
            whoop.Play();
    }
}
