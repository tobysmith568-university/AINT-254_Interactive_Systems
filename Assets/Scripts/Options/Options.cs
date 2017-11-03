using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    private void Start()
    {
        //Show the first panel
        ShowPanel0();

        /* Get all the options from their player prefs */

        //General
        xSlider.value = MyPrefs.XSensitivity;
        ySlider.value = MyPrefs.YSensitivity;

        xToggle.isOn = MyPrefs.XAxisInverted;
        yToggle.isOn = MyPrefs.YAxisInverted;

        redSlider.value = MyPrefs.CrosshairRed;
        greenSlider.value = MyPrefs.CrosshairGreen;
        blueSlider.value = MyPrefs.CrosshairBlue;
    }

    /// <summary>
    /// Code for switching between the different panels in the scene
    /// </summary>
    #region Panels
    [SerializeField]
    GameObject[] panels;

    int currentPanel = -1;

    /// <summary>
    /// Hides all the panels in the scene
    /// </summary>
    private void HideAllPanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }

    /// <summary>
    /// Shows only the panel '0'
    /// Called by the panel's button
    /// </summary>
    public void ShowPanel0()
    {
        if (currentPanel == 0)
            return;
        HideAllPanels();
        panels[0].SetActive(true);
        currentPanel = 0;
    }

    /// <summary>
    /// Shows only the panel '1'
    /// Called by the panel's button
    /// </summary>
    public void ShowPanel1()
    {
        if (currentPanel == 1)
            return;
        HideAllPanels();
        panels[1].SetActive(true);
        currentPanel = 1;
    }

    /// <summary>
    /// Shows only the panel '2'
    /// Called by the panel's button
    /// </summary>
    public void ShowPanel2()
    {
        if (currentPanel == 2)
            return;
        HideAllPanels();
        panels[2].SetActive(true);
        currentPanel = 2;
    }

    /// <summary>
    /// Shows only the panel '3'
    /// Called by the panel's button
    /// </summary>
    public void ShowPanel3()
    {
        if (currentPanel == 3)
            return;
        HideAllPanels();
        panels[3].SetActive(true);
        currentPanel = 3;
    }

    /// <summary>
    /// Called by the main menu button to load the main menu scene
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
    #endregion

    /// <summary>
    /// Code for the general tab (0)
    /// </summary>
    #region General
    [SerializeField]
    Text xValue;
    [SerializeField]
    Text yValue;

    [SerializeField]
    Slider xSlider;
    [SerializeField]
    Slider ySlider;

    [SerializeField]
    Toggle xToggle;
    [SerializeField]
    Toggle yToggle;

    [SerializeField]
    Image crosshair;

    [SerializeField]
    Slider redSlider;
    [SerializeField]
    Slider greenSlider;
    [SerializeField]
    Slider blueSlider;

    /// <summary>
    /// Called when the X Axis sensitivity is changed
    /// </summary>
    public void XAxisChanged()
    {
        xValue.text = ((int)(25 * xSlider.value - 25)).ToString();
        MyPrefs.XSensitivity = xSlider.value;
    }

    /// <summary>
    /// Called when the Y Axis sensitivity is changed
    /// </summary>
    public void YAxisChanged()
    {
        yValue.text = ((int)(25 * ySlider.value - 25)).ToString();
        MyPrefs.YSensitivity = ySlider.value;
    }

    /// <summary>
    /// Called when the X Axis look inversion is changed
    /// </summary>
    public void XToggled()
    {
        MyPrefs.XAxisInverted = xToggle.isOn;
    }

    /// <summary>
    /// Called when the Y Axis look inversion is changed
    /// </summary>
    public void YToggled()
    {
        MyPrefs.YAxisInverted = yToggle.isOn;
    }

    /// <summary>
    /// Called when the crosshairs red value is changed
    /// </summary>
    public void RedChanged()
    {
        crosshair.color = new Color(redSlider.value, crosshair.color.g, crosshair.color.b);
        MyPrefs.CrosshairRed = redSlider.value;
    }

    /// <summary>
    /// Called when the crosshairs green value is changed
    /// </summary>
    public void GreenChanged()
    {
        crosshair.color = new Color(crosshair.color.r, greenSlider.value, crosshair.color.b);
        MyPrefs.CrosshairGreen = greenSlider.value;
    }

    /// <summary>
    /// Called when the crosshairs blue value is changed
    /// </summary>
    public void BlueChanged()
    {
        crosshair.color = new Color(crosshair.color.r, crosshair.color.g, blueSlider.value);
        MyPrefs.CrosshairBlue = blueSlider.value;
    }
    #endregion
}