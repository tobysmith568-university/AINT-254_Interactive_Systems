using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    private void Start()
    {
        //Panels
        ShowPanel0();

        //General
        if (!MyPrefs.HasFloat(FloatPref.XSensitivity))
            MyPrefs.SetFloat(FloatPref.XSensitivity, 3);
        if (!MyPrefs.HasFloat(FloatPref.YSensitivity))
            MyPrefs.SetFloat(FloatPref.YSensitivity, 3);

        xSlider.value = MyPrefs.GetFloat(FloatPref.XSensitivity);
        ySlider.value = MyPrefs.GetFloat(FloatPref.YSensitivity);
    }

    #region Panels
    [SerializeField]
    GameObject[] panels;

    int currentPanel = -1;

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
    #endregion

    #region General
    [SerializeField]
    Text xValue;
    [SerializeField]
    Text yValue;

    [SerializeField]
    Slider xSlider;
    [SerializeField]
    Slider ySlider;

    public void XAxisChanged()
    {
        xValue.text = ((int)(25 * xSlider.value - 25)).ToString();
        MyPrefs.SetFloat(FloatPref.XSensitivity, xSlider.value);
    }

    public void YAxisChanged()
    {
        yValue.text = ((int)(25 * ySlider.value - 25)).ToString();
        MyPrefs.SetFloat(FloatPref.YSensitivity, ySlider.value);
    }
    #endregion
}
    