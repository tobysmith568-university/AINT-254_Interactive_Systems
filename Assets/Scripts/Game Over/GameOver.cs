using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net;
using UnityEngine.Networking;

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

    GameScore[] timeScores;
    GameScore[] scoreScores;
    GameScore gameScore;

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
        menuButton.interactable = playerName.text.Length > 0;

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

        StartCoroutine(GetText());

        //Save the new highscores
        MyPrefs.LowTimes = timeScores;
        MyPrefs.HighScores = scoreScores;
        
        //Switch the scene
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    IEnumerator GetText()
    {
        string url = "http://tobysmith.uk/ShooterUnknown/SendScore.php?" +
            "pass=we345678i9olkjhgtrewazsxcvbnjkio90pokjyt432waw23erfr567ujhg" +
            "&score={\"Name\":\"" + gameScore.Name + "\",\"Duration\":" + gameScore.Duration + ",\"Score\":" + gameScore.Score + "}";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.Send();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
    }

        int index = 0;
    public void TitleEnter()
    {
        Color color;
        string[] colours = { "#FFB32AFF", "#33D311FF", "#008EE3FF", "#A216D4FF" };
        ColorUtility.TryParseHtmlString(colours[index], out color);
        title.color = color;
        index = (index == colours.Length - 1) ? 0 : index + 1;
    }

    public void TitleExit()
    {
        Color color;
        ColorUtility.TryParseHtmlString("#AD4043FF", out color);
        title.color = color;
    }
}
