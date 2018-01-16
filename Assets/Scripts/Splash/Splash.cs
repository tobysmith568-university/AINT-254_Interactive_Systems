using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //If any key is pressed, run the animation to load the main menu scene
        if (Input.anyKeyDown)
            anim.SetTrigger("LoadMenu");
    }

    /// <summary>
    /// Loads the main menu scene
    /// </summary>
    public void MainMenu()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(1);
    }
}