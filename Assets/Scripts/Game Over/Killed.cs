using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Killed : MonoBehaviour
{
    [SerializeField]
    Image title;

    [SerializeField]
    AudioSource whoop;

    private void Start()
    {
        TitleExit();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(1);
    }

    int index = 0;
    public void TitleEnter()
    {
        Color color;
        string[] colours = { "#FFB32AFF", "#33D311FF", "#008EE3FF", "#A216D4FF" };
        ColorUtility.TryParseHtmlString(colours[index], out color);
        title.color = color;
        index = (index == colours.Length - 1) ? 0 : index + 1;

        Whoop();
    }

    public void TitleExit()
    {
        Color color;
        ColorUtility.TryParseHtmlString("#AD4043FF", out color);
        title.color = color;
    }

    public void Whoop()
    {
        if (!whoop.isPlaying)
            whoop.Play();
    }
}