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
    Image title;
    [SerializeField]
    AudioMixer mixer;
    [SerializeField]
    AudioSource whoop;

    int index = 0;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        mixer.SetFloat("master", MyPrefs.MasterVolume);
        mixer.SetFloat("player", MyPrefs.PlayerVolume);
        mixer.SetFloat("targets", MyPrefs.TargetsVolume);
        mixer.SetFloat("UI", MyPrefs.UIVolume);

        TitleExit();
    }

    /// <summary>
    /// Changes the colour of the title when rolled-over
    /// </summary>
    public void TitleEnter()
    {
        string[] colours = { "#FFB32AFF", "#33D311FF", "#008EE3FF", "#A216D4FF" };
        
        Color color;
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

    /// <summary>
    /// Quits the application
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}
