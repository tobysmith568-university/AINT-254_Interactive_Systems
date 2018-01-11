using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Newtonsoft.Json;

public static class MyPrefs
{
    public enum Prefs
    {
        HighScores,
        LowTimes,
        LastPlay,
        XSensitivity,
        YSensitivity,
        CrosshairRed,
        CrosshairGreen,
        CrosshairBlue,
        KeyMappings,
        XAxisInverted,
        YAxisInverted,
        Resolution,
        Fullscreen
    }

    public static GameScore[] HighScores
    {
        get
        {
            return JsonConvert.DeserializeObject<GameScore[]>(PlayerPrefs.GetString("HighScores"));
        }
        set
        {
            PlayerPrefs.SetString("HighScores", JsonConvert.SerializeObject(value));
            PlayerPrefs.Save();
        }
    }
    public static GameScore[] LowTimes
    {
        get
        {
            return JsonConvert.DeserializeObject<GameScore[]>(PlayerPrefs.GetString("LowTimes"));
        }
        set
        {
            PlayerPrefs.SetString("LowTimes", JsonConvert.SerializeObject(value));
            PlayerPrefs.Save();
        }
    }
    public static GameScore LastPlay
    {
        get
        {
            return JsonConvert.DeserializeObject<GameScore>(PlayerPrefs.GetString("LastPlay"));
        }
        set
        {
            PlayerPrefs.SetString("LastPlay", JsonConvert.SerializeObject(value));
            PlayerPrefs.Save();
        }
    }
    public static float XSensitivity
    {
        get
        {
            return PlayerPrefs.GetFloat("XSensitivity");
        }
        set
        {
            PlayerPrefs.SetFloat("XSensitivity", value);
            PlayerPrefs.Save();
        }
    }
    public static float YSensitivity
    {
        get
        {
            return PlayerPrefs.GetFloat("YSensitivity");
        }
        set
        {
            PlayerPrefs.SetFloat("YSensitivity", value);
            PlayerPrefs.Save();
        }
    }
    public static float CrosshairRed
    {
        get
        {
            return PlayerPrefs.GetFloat("CrosshairRed");
        }
        set
        {
            PlayerPrefs.SetFloat("CrosshairRed", value);
            PlayerPrefs.Save();
        }
    }
    public static float CrosshairGreen
    {
        get
        {
            return PlayerPrefs.GetFloat("CrosshairGreen");
        }
        set
        {
            PlayerPrefs.SetFloat("CrosshairGreen", value);
            PlayerPrefs.Save();
        }
    }
    public static float CrosshairBlue
    {
        get
        {
            return PlayerPrefs.GetFloat("CrosshairBlue");
        }
        set
        {
            PlayerPrefs.SetFloat("CrosshairBlue", value);
            PlayerPrefs.Save();
        }
    }
    public static Mapping[] KeyMappings
    {
        get
        {
            return JsonConvert.DeserializeObject<Mapping[]>(PlayerPrefs.GetString("KeyMappings"));
        }
        set
        {
            PlayerPrefs.SetString("KeyMappings", JsonConvert.SerializeObject(value));
            PlayerPrefs.Save();
        }
    }
    public static bool XAxisInverted
    {
        get
        {
            return PlayerPrefs.GetInt("XAxisInverted") == 1;
        }
        set
        {
            PlayerPrefs.SetInt("XAxisInverted", value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
    public static bool YAxisInverted
    {
        get
        {
            return PlayerPrefs.GetInt("YAxisInverted") == 1;
        }
        set
        {
            PlayerPrefs.SetInt("YAxisInverted", value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
    public static Resolution Resolution
    {
        get
        {
            return JsonConvert.DeserializeObject<Resolution>(PlayerPrefs.GetString("Resolution"));
        }
        set
        {
            PlayerPrefs.SetString("Resolution", JsonConvert.SerializeObject(value));
            PlayerPrefs.Save();
        }
    }
    public static bool Fullscreen
    {
        get
        {
            return PlayerPrefs.GetInt("Fullscreen") == 1;
        }
        set
        {
            PlayerPrefs.SetInt("Fullscreen", value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    static MyPrefs()
    {
        //If any prefs don't exsist, give them their default values
        if (!Exists(Prefs.HighScores))
            HighScores = new GameScore[]
            {
                new GameScore(), new GameScore(), new GameScore(), new GameScore(), new GameScore(),
                new GameScore(), new GameScore(), new GameScore(), new GameScore(), new GameScore()
            };
        if (!Exists(Prefs.LowTimes))
            LowTimes = new GameScore[]
            {
                new GameScore(), new GameScore(), new GameScore(), new GameScore(), new GameScore(),
                new GameScore(), new GameScore(), new GameScore(), new GameScore(), new GameScore()
            };
        if (!Exists(Prefs.LastPlay))
            LastPlay = new GameScore();
        if (!Exists(Prefs.XSensitivity))
            XSensitivity = 2f;
        if (!Exists(Prefs.YSensitivity))
            YSensitivity = 2f;
        if (!Exists(Prefs.CrosshairRed))
            CrosshairRed = 0f;
        if (!Exists(Prefs.CrosshairGreen))
            CrosshairGreen = 0f;
        if (!Exists(Prefs.CrosshairBlue))
            CrosshairBlue = 0f;
        if (!Exists(Prefs.CrosshairBlue))
            CrosshairBlue = 0f;
        //Prefs.KeyMappings is set in MyInput.cs so isn't set here
        if (!Exists(Prefs.XAxisInverted))
            XAxisInverted = false;
        if (!Exists(Prefs.YAxisInverted))
            YAxisInverted = false;
        if (!Exists(Prefs.Resolution))
            Resolution = Screen.currentResolution;
        if (!Exists(Prefs.YAxisInverted))
            YAxisInverted = Screen.fullScreen;
    }

    /// <summary>
    /// Deletes a PlayerPref
    /// Note: It will still be in the pref Enum
    /// </summary>
    /// <param name="pref">The name of the PlayerPref</param>
    public static void Delete(Prefs pref)
    {
        PlayerPrefs.DeleteKey(pref.ToString());
    }

    /// <summary>
    /// Tests to see if a PlayerPref has a value
    /// </summary>
    /// <param name="pref">The name of the PlayerPref</param>
    /// <returns>True if it does have a value</returns>
    public static bool Exists(Prefs pref)
    {
        return PlayerPrefs.HasKey(pref.ToString());
    }
}
