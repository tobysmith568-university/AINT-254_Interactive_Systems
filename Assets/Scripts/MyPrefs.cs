using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IntPref
{
    Time,
    Score
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
    /// Returns the value of a boolean PlayerPref
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
    /// <summary>
    /// Sets an interger PlayerPref
    /// </summary>
    /// <param name="pref">The PlayerPref to set</param>
    /// <param name="value">The value to set the PlayerPref to</param>
    public static void SetInt(IntPref pref, int value)
    {
        doesExist(pref.ToString());
        PlayerPrefs.SetInt(pref.ToString(), value);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Sets a float PlayerPref
    /// </summary>
    /// <param name="pref">The PlayerPref to set</param>
    /// <param name="value">The value to set the PlayerPref to</param>
    public static void SetFloat(FloatPref pref, float value)
    {
        doesExist(pref.ToString());
        PlayerPrefs.SetFloat(pref.ToString(), value);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Sets a string PlayerPref
    /// </summary>
    /// <param name="pref">The PlayerPref to set</param>
    /// <param name="value">The value to set the PlayerPref to</param>
    public static void SetString(StringPref pref, string value)
    {
        doesExist(pref.ToString());
        PlayerPrefs.SetString(pref.ToString(), value);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Sets a boolean PlayerPref
    /// </summary>
    /// <param name="pref">The PlayerPref to set</param>
    /// <param name="value">The value to set the PlayerPref to</param>
    public static void SetBool(BoolPref pref, bool value)
    {
        doesExist(pref.ToString());
        PlayerPrefs.SetInt(pref.ToString(), value ? 1 : 0);
        PlayerPrefs.Save();
    }
    #endregion
    #region Has
    /// <summary>
    /// Tests to see if an integer PlayerPref exsists
    /// </summary>
    /// <param name="pref">The name of the PlayerPref</param>
    /// <returns>True if it does exist</returns>
    public static bool HasInt(IntPref pref)
    {
        return PlayerPrefs.HasKey(pref.ToString());
    }

    /// <summary>
    /// Tests to see if a float PlayerPref exsists
    /// </summary>
    /// <param name="pref">The name of the PlayerPref</param>
    /// <returns>True if it does exist</returns>
    public static bool HasFloat(FloatPref pref)
    {
        return PlayerPrefs.HasKey(pref.ToString());
    }

    /// <summary>
    /// Tests to see if a string PlayerPref exsists
    /// </summary>
    /// <param name="pref">The name of the PlayerPref</param>
    /// <returns>True if it does exist</returns>
    public static bool HasString(StringPref pref)
    {
        return PlayerPrefs.HasKey(pref.ToString());
    }

    /// <summary>
    /// Tests to see if a boolean PlayerPref exsists
    /// </summary>
    /// <param name="pref">The name of the PlayerPref</param>
    /// <returns>True if it does exist</returns>
    public static bool HasBool(BoolPref pref)
    {
        return (PlayerPrefs.HasKey(pref.ToString()));
    }
    #endregion
    #region Delete
    /// <summary>
    /// Deletes an integer PlayerPref
    /// Note: it will still stay in the IntPref enum
    /// </summary>
    /// <param name="pref">The name of the PlayerPref to delete</param>
    public static void DeleteInt(IntPref pref)
    {
        doesExist(pref.ToString());
        PlayerPrefs.DeleteKey(pref.ToString());
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Deletes a float PlayerPref
    /// Note: it will still stay in the IntPref enum
    /// </summary>
    /// <param name="pref">The name of the PlayerPref to delete</param>
    public static void DeleteFloat(FloatPref pref)
    {
        doesExist(pref.ToString());
        PlayerPrefs.DeleteKey(pref.ToString());
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Deletes a string PlayerPref
    /// Note: it will still stay in the IntPref enum
    /// </summary>
    /// <param name="pref">The name of the PlayerPref to delete</param>
    public static void DeleteString(StringPref pref)
    {
        doesExist(pref.ToString());
        PlayerPrefs.DeleteKey(pref.ToString());
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Deletes a boolean PlayerPref
    /// Note: it will still stay in the IntPref enum
    /// </summary>
    /// <param name="pref">The name of the PlayerPref to delete</param>
    public static void DeleteBool(BoolPref pref)
    {
        doesExist(pref.ToString());
        PlayerPrefs.DeleteKey(pref.ToString());
        PlayerPrefs.Save();
    }
    #endregion
}
