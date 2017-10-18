using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField]
    GameObject[] panels;

    int currentPanel = -1;

    private void Start()
    {
        ShowPanel0();
    }

    private void HideAllPanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }

    public void ShowPanel0()
    {
        if (currentPanel == 0)
            return;
        HideAllPanels();
        panels[0].SetActive(true);
        currentPanel = 0;
    }

    public void ShowPanel1()
    {
        if (currentPanel == 1)
            return;
        HideAllPanels();
        panels[1].SetActive(true);
        currentPanel = 1;
    }

    public void ShowPanel2()
    {
        if (currentPanel == 2)
            return;
        HideAllPanels();
        panels[2].SetActive(true);
        currentPanel = 2;
    }

    public void ShowPanel3()
    {
        if (currentPanel == 3)
            return;
        HideAllPanels();
        panels[3].SetActive(true);
        currentPanel = 3;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
