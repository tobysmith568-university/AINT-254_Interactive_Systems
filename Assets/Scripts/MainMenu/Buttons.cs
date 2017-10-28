using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    /// <summary>
    /// Called by the Play button to load the next level
    /// </summary>
    public void Play()
    {
        SceneManager.LoadScene(4, LoadSceneMode.Single);
    }

    /// <summary>
    /// Called by the Options button to load the options scene
    /// </summary>
    public void Options()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}
