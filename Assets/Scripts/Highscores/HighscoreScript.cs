using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net;
using Newtonsoft.Json;
using UnityEngine.Networking;

/// <summary>
/// THIS SCRIPT IS ADDED TO THE EVENT LISTENER GAMEOBJECT IN THE SCENE!
/// </summary>
public class HighscoreScript : MonoBehaviour
{
    [SerializeField]
    Text[] localScores;
    [SerializeField]
    Text[] localTimes;
    [SerializeField]
    Text[] globalScores;
    [SerializeField]
    Text[] globalTimes;

    [SerializeField]
    Image title;

    void Start()
    {
        StartCoroutine(GetText());

        for (int i = 0; i < 10; i++)
        {
            if (MyPrefs.HighScores[i].Score != 0)
                localScores[i].text = MyPrefs.HighScores[i].Name + " : " + MyPrefs.HighScores[i].Score;
            if (MyPrefs.LowTimes[i].Duration != 0)
                localTimes[i].text = MyPrefs.LowTimes[i].Name + " : " + new System.DateTime().AddMilliseconds(MyPrefs.LowTimes[i].Duration).ToString("m:ss:f");
        }

        TitleExit();
    }

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://tobysmith.uk/ShooterUnknown/SendScore.php?pass=we345678i9olkjhgtrewazsxcvbnjkio90pokjyt432waw23erfr567ujhg");
        yield return www.Send();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            ScoreDownload scores = JsonConvert.DeserializeObject<ScoreDownload>(www.downloadHandler.text);

            for (int i = 0; i < 10; i++)
            {
                if (scores.ScoreScores[i].Score != 0)
                    globalScores[i].text = scores.ScoreScores[i].Name + " : " + scores.ScoreScores[i].Score;
                if (scores.TimeScores[i].Duration != 0)
                    globalTimes[i].text = scores.TimeScores[i].Name + " : " + new System.DateTime().AddMilliseconds(scores.TimeScores[i].Duration).ToString("m:ss:f");
            }
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

    /// <summary>
    /// Called by the MainMenu button to load the main menu scene
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
