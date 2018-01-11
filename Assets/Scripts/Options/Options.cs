using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// THIS SCRIPT IS ADDED TO THE EVENT LISTENER GAMEOBJECT IN THE SCENE!
/// </summary>

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

        //Video
        fullscreenToggle.isOn = Screen.fullScreen;

        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            options.Add(new Dropdown.OptionData(Screen.resolutions[i].width + "x" + Screen.resolutions[i].height + " " + Screen.resolutions[i].refreshRate + "Hz"));
        }
        ResolutionDropdown.AddOptions(options);
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].width == Screen.currentResolution.width
                        && Screen.resolutions[i].height == Screen.currentResolution.height)
                ResolutionDropdown.value = i;
        }

        //Controls
        UpdateButtonText();
    }

    private void Update()
    {
        MappingPanel.SetActive(currentControl != null);

        //Controls
        if (currentControl != null)
        {
            PrimaryMappingPanel.SetActive(primaryOrSecondary == 0);
            SecondaryMappingPanel.SetActive(primaryOrSecondary != 0);

            PrimaryMappingText.text = SecondaryMappingText.text = "Press the new " + (primaryOrSecondary == 0 ? "primary" : "secondary") + " mapping for: " + currentControl.Value;

            if (Input.GetKeyDown(KeyCode.Escape) && primaryOrSecondary == 1)
                Invoke("RemoveMapping", 1f);
            if (Input.GetKeyUp(KeyCode.Escape) && primaryOrSecondary == 1)
                CancelInvoke("RemoveMapping");

            //For every possible key input
            foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
            {
                //If it's being pressed
                if ((Input.GetKeyDown(kcode) && kcode != KeyCode.Escape) || (Input.GetKeyUp(kcode) && kcode == KeyCode.Escape))
                {
                    //Map the buttons input to that key
                    if (primaryOrSecondary == 0)
                        MyInput.SetKeyMap(control: (Control)currentControl, primaryKey: kcode);
                    else
                        MyInput.SetKeyMap(control: (Control)currentControl, secondaryKey: kcode);

                    UpdateButtonText();

                    currentControl = null;
                }
            }
        }
    }

    #region Panel switching
    [Header("Panel switching")]
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


    #region General tab
    [Header("General tab")]
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

    #region Video tab
    [Header("Video tab")]
    [SerializeField]
    Toggle fullscreenToggle;
    [SerializeField]
    Dropdown ResolutionDropdown;

    public void FullscreenToggled()
    {
        Screen.fullScreen = MyPrefs.Fullscreen = fullscreenToggle.isOn;
    }

    public void ResolutionChanged()
    {
        Screen.SetResolution(Screen.resolutions[ResolutionDropdown.value].width, Screen.resolutions[ResolutionDropdown.value].height, Screen.fullScreen);
        MyPrefs.Resolution = Screen.currentResolution;
    }

    #endregion


    #region Controls tab
    [Header("Controls tab")]
    [SerializeField]
    Text[] ShootMappings;
    [SerializeField]
    Text[] ScopeMappings;
    [SerializeField]
    Text[] ForwardMappings;
    [SerializeField]
    Text[] BackwardMappings;
    [SerializeField]
    Text[] LeftMappings;
    [SerializeField]
    Text[] RightMappings;
    [SerializeField]
    Text[] SprintMappings;
    [SerializeField]
    Text[] CrouchMappings;
    [SerializeField]
    Text[] JumpMappings;
    [SerializeField]
    Text[] ReloadMappings;
    [SerializeField]
    Text[] PauseMappings;

    [SerializeField]
    GameObject MappingPanel;
    [SerializeField]
    GameObject PrimaryMappingPanel;
    [SerializeField]
    Text PrimaryMappingText;
    [SerializeField]
    GameObject SecondaryMappingPanel;
    [SerializeField]
    Text SecondaryMappingText;

    Control? currentControl;
    int primaryOrSecondary = 0;

    public void MappingPressed(string mapping)
    {
        currentControl = (Control)System.Enum.Parse(typeof(Control), mapping.Split(' ')[0]);
        primaryOrSecondary = int.Parse(mapping.Split(' ')[1]);
    }

    public void UpdateButtonText()
    {
        foreach (Mapping mapping in MyInput.keyMaps)
        {
            switch (mapping.Name)
            {
                case "Shoot":
                    ShootMappings[0].text = mapping.PrimaryInput.ToString();
                    ShootMappings[1].text = mapping.SecondaryInput.ToString();
                    break;
                case "Scope":
                    ScopeMappings[0].text = mapping.PrimaryInput.ToString();
                    ScopeMappings[1].text = mapping.SecondaryInput.ToString();
                    break;
                case "Forward":
                    ForwardMappings[0].text = mapping.PrimaryInput.ToString();
                    ForwardMappings[1].text = mapping.SecondaryInput.ToString();
                    break;
                case "Backward":
                    BackwardMappings[0].text = mapping.PrimaryInput.ToString();
                    BackwardMappings[1].text = mapping.SecondaryInput.ToString();
                    break;
                case "Left":
                    LeftMappings[0].text = mapping.PrimaryInput.ToString();
                    LeftMappings[1].text = mapping.SecondaryInput.ToString();
                    break;
                case "Right":
                    RightMappings[0].text = mapping.PrimaryInput.ToString();
                    RightMappings[1].text = mapping.SecondaryInput.ToString();
                    break;
                case "Sprint":
                    SprintMappings[0].text = mapping.PrimaryInput.ToString();
                    SprintMappings[1].text = mapping.SecondaryInput.ToString();
                    break;
                case "Crouch":
                    CrouchMappings[0].text = mapping.PrimaryInput.ToString();
                    CrouchMappings[1].text = mapping.SecondaryInput.ToString();
                    break;
                case "Jump":
                    JumpMappings[0].text = mapping.PrimaryInput.ToString();
                    JumpMappings[1].text = mapping.SecondaryInput.ToString();
                    break;
                case "Reload":
                    ReloadMappings[0].text = mapping.PrimaryInput.ToString();
                    ReloadMappings[1].text = mapping.SecondaryInput.ToString();
                    break;
                case "Pause":
                    PauseMappings[0].text = mapping.PrimaryInput.ToString();
                    PauseMappings[1].text = mapping.SecondaryInput.ToString();
                    break;
                default:
                    break;
            }
        }
    }

    public void ResetMappings()
    {
        MyInput.ResetMappings();
        UpdateButtonText();
    }

    private void RemoveMapping()
    {
        MyInput.RemoveSecondMapping((Control)currentControl);
        UpdateButtonText();
        currentControl = null;
    }
    
    #endregion
}