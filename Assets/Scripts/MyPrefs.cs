using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IntPref
{
}

public enum FloatPref
{
    XSensitivity,
    YSensitivity,
    CrosshairRed,
    CrosshairGreen,
    CrosshairBlue
}

public enum StringPref
{
    primaryInputs,
    secondryInputs
}

public enum BoolPref
{
    xInverted,
    yInverted
}

public static class MyPrefs
{
    static MyPrefs()
    {
        //foreach (IntPref pref in System.Enum.GetValues(typeof(IntPref)))
        //{
        //    if (!PlayerPrefs.HasKey(pref.ToString()))
        //        PlayerPrefs.SetInt(pref.ToString(), 0);
        //}
        //foreach (FloatPref pref in System.Enum.GetValues(typeof(FloatPref)))
        //{
        //    if (!PlayerPrefs.HasKey(pref.ToString()))
        //        PlayerPrefs.SetFloat(pref.ToString(), 0f);
        //}
        //foreach (StringPref pref in System.Enum.GetValues(typeof(StringPref)))
        //{
        //    if (!PlayerPrefs.HasKey(pref.ToString()))
        //        PlayerPrefs.SetString(pref.ToString(), "");
        //}
        //foreach (BoolPref pref in System.Enum.GetValues(typeof(BoolPref)))
        //{
        //    if (!PlayerPrefs.HasKey(pref.ToString()))
        //        PlayerPrefs.SetInt(pref.ToString(), 0);
        //}
    }

    static void doesExist(string pref)
    {
        //if (!PlayerPrefs.HasKey(pref))
        //    throw new System.Exception("Pref doesn't exist!");
    }

    #region Getters
    /// <summary>
    /// Returns the value of an integer PlayerPref
    /// </summary>
    /// <param name="pref">The name of the PlayerPref</param>
    /// <returns>The PlayerPrefs value</returns>
    public static int GetInt(IntPref pref)
    {
        doesExist(pref.ToString());
        return PlayerPrefs.GetInt(pref.ToString());
    }

    /// <summary>
    /// Returns the value of a float PlayerPref
    /// </summary>
    /// <param name="pref">The name of the PlayerPref</param>
    /// <returns>The PlayerPrefs value</returns>
    public static float GetFloat(FloatPref pref)
    {
        doesExist(pref.ToString());
        return PlayerPrefs.GetFloat(pref.ToString());
    }

    /// <summary>
    /// Returns the value of a string PlayerPref
    /// </summary>
    /// <param name="pref">The name of the PlayerPref</param>
    /// <returns>The PlayerPrefs value</returns>
    public static string GetString(StringPref pref)
    {
        doesExist(pref.ToString());
        return PlayerPrefs.GetString(pref.ToString());
    }

    /// <summary>
    /// Returns the value of a bool PlayerPref
    /// </summary>
    /// <param name="pref">The name of the PlayerPref</param>
    /// <returns>The PlayerPrefs value</returns>
    public static bool GetBool(BoolPref pref)
    {
        doesExist(pref.ToString());
        return (PlayerPrefs.GetInt(pref.ToString()) == 0) ? false : true;
    }
    #endregion
    #region Setters
    public static void SetInt(IntPref pref, int value)
    {
        doesExist(pref.ToString());
        PlayerPrefs.SetInt(pref.ToString(), value);
        PlayerPrefs.Save();
    }

    public static void SetFloat(FloatPref pref, float value)
    {
        doesExist(pref.ToString());
        PlayerPrefs.SetFloat(pref.ToString(), value);
        PlayerPrefs.Save();
    }

    public static void SetString(StringPref pref, string value)
    {
        doesExist(pref.ToString());
        PlayerPrefs.SetString(pref.ToString(), value);
        PlayerPrefs.Save();
    }

    public static void SetBool(BoolPref pref, bool value)
    {
        doesExist(pref.ToString());
        PlayerPrefs.SetInt(pref.ToString(), value ? 1 : 0);
        PlayerPrefs.Save();
    }
    #endregion
    #region Has
    public static bool HasInt(IntPref pref)
    {
        return PlayerPrefs.HasKey(pref.ToString());
    }

    public static bool HasFloat(FloatPref pref)
    {
        return PlayerPrefs.HasKey(pref.ToString());
    }

    public static bool HasString(StringPref pref)
    {
        return PlayerPrefs.HasKey(pref.ToString());
    }

    public static bool HasBool(BoolPref pref)
    {
        return (PlayerPrefs.HasKey(pref.ToString()));
    }
    #endregion
    #region Delete
    public static void DeleteInt(IntPref pref)
    {
        doesExist(pref.ToString());
        PlayerPrefs.DeleteKey(pref.ToString());
        PlayerPrefs.Save();
    }

    public static void DeleteFloat(FloatPref pref)
    {
        doesExist(pref.ToString());
        PlayerPrefs.DeleteKey(pref.ToString());
        PlayerPrefs.Save();
    }

    public static void DeleteString(StringPref pref)
    {
        doesExist(pref.ToString());
        PlayerPrefs.DeleteKey(pref.ToString());
        PlayerPrefs.Save();
    }

    public static void DeleteBool(BoolPref pref)
    {
        doesExist(pref.ToString());
        PlayerPrefs.DeleteKey(pref.ToString());
        PlayerPrefs.Save();
    }
    #endregion
}
