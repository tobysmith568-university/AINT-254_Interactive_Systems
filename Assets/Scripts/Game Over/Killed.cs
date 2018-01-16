using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// THIS SCRIPT IS ADDED TO THE EVENT LISTENER GAMEOBJECT IN THE SCENE!
/// </summary>
public class Killed : MonoBehaviour
{
    [SerializeField]
    Image title;

    [SerializeField]
    AudioSource whoop;

    int index = 0;

    private void Start()
    {
        TitleExit();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /// <summary>
    /// Loads the Main Menu scene
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene(1);
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