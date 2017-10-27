using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(4, LoadSceneMode.Single);
    }

    public void Options()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}
