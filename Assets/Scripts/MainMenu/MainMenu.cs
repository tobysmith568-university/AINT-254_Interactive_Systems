using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

/// <summary>
/// THIS SCRIPT IS ADDED TO THE EVENT LISTENER GAMEOBJECT IN THE SCENE!
/// </summary>
public class MainMenu : MonoBehaviour
{
    [SerializeField]
    bool DeleteAllPrefs = false;
    [SerializeField]
    Image title;
    [SerializeField]
    AudioMixer mixer;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (DeleteAllPrefs)
            PlayerPrefs.DeleteAll();
        
        mixer.SetFloat("master", MyPrefs.MasterVolume);
        mixer.SetFloat("player", MyPrefs.PlayerVolume);
        mixer.SetFloat("targets", MyPrefs.TargetsVolume);
        mixer.SetFloat("UI", MyPrefs.UIVolume);

        TitleExit();
    }

    int index = 0;
    public void TitleEnter()
    {
        string[] colours = { "#FFB32AFF", "#33D311FF", "#008EE3FF", "#A216D4FF" };
        
        Color color;
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
    /// Called by the Play button to load the next level
    /// </summary>
    public void Play()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    /// <summary>
    /// Called by the Leaderboard button to load the leaderboard scene
    /// </summary>
    public void Leaderboard()
    {
        SceneManager.LoadScene(6, LoadSceneMode.Single);
    }

    /// <summary>
    /// Called by the Options button to load the options scene
    /// </summary>
    public void Options()
    {
        SceneManager.LoadScene(5, LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
