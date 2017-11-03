using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// THIS SCRIPT IS ADDED TO THE EVENT LISTENER GAMEOBJECT IN THE SCENE!
/// </summary>
public class MainMenu : MonoBehaviour
{
    [SerializeField]
    bool DeleteAllPrefs = false;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (DeleteAllPrefs)
            PlayerPrefs.DeleteAll();
    }

    /// <summary>
    /// Called by the Play button to load the next level
    /// </summary>
    public void Play()
    {
        SceneManager.LoadScene(4, LoadSceneMode.Single);
    }

    /// <summary>
    /// Called by the Leaderboard button to load the leaderboard scene
    /// </summary>
    public void Leaderboard()
    {
        SceneManager.LoadScene(5, LoadSceneMode.Single);
    }

    /// <summary>
    /// Called by the Options button to load the options scene
    /// </summary>
    public void Options()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
