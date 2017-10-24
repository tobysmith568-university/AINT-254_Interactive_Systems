using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum Control
{
    Shoot,
    Scope,
    Forward,
    Backward,
    Left,
    Right,
    Sprint,
    Crouch,
    Jump,
    Pause
}

public class Mapping
{
    public string Name { get; set; }
    public KeyCode PrimaryInput { get; set; }
    public KeyCode? SecondryInput { get; set; }

    public Mapping(string name, KeyCode primaryInput, KeyCode? secondryInput = null)
    {
        Name = name;
        PrimaryInput = primaryInput;
        SecondryInput = secondryInput;
    }
}
public class MyInput : MonoBehaviour
{
    public static List<Mapping> keyMaps = new List<Mapping>();

    static string defaultPrimaryKeys = "Mouse0|Mouse1|W|S|A|D|LeftShift|LeftControl|Space|Escape";
    static string defaultSecondryKeys = "null|null|UpArrow|DownArrow|LeftArrow|RightArrow|null|null|null|P";

    static MyInput()
    {
        //DEBUG LINES -------------------------------------------------------------------------------------------------------------------------- NEEDS TO BE REMOVED FOR INPUTS TO BE SETTABLE
        MyPrefs.DeleteString(StringPref.primaryInputs);
        MyPrefs.DeleteString(StringPref.secondryInputs);
        //END DEBUG LINES

        //If the PlayerPref for the inputs cannot be found, add the defaults
        if (!MyPrefs.HasString(StringPref.primaryInputs)) MyPrefs.SetString(StringPref.primaryInputs, defaultPrimaryKeys);
        if (!MyPrefs.HasString(StringPref.secondryInputs)) MyPrefs.SetString(StringPref.secondryInputs, defaultSecondryKeys);

        InitializeMaps();
    }

    /// <summary>
    /// This method takes the saved PlayerPref for the inputs and sets up
    /// dictionary entries in the key map dictionary for each
    /// </summary>
    private static void InitializeMaps()
    {
        string[] primaryButtonStings = MyPrefs.GetString(StringPref.primaryInputs).Split('|');
        string[] secondryButtonStings = MyPrefs.GetString(StringPref.secondryInputs).Split('|');

        for (int i = 0; i < Enum.GetValues(typeof(Control)).Length; i++)
        {
            if (secondryButtonStings[i] == "null")
                keyMaps.Add(new Mapping(
                    ((Control)Enum.GetValues(typeof(Control)).GetValue(i)).ToString(),
                    (KeyCode)Enum.Parse(typeof(KeyCode), primaryButtonStings[i])));
            else
                keyMaps.Add(new Mapping(
                    ((Control)Enum.GetValues(typeof(Control)).GetValue(i)).ToString(),
                    (KeyCode)Enum.Parse(typeof(KeyCode), primaryButtonStings[i]),
                    (KeyCode)Enum.Parse(typeof(KeyCode), secondryButtonStings[i])));
        }
    }

    /// <summary>
    /// Takes each entry in the key map dictionary and saves a single concatenated string in the PlayerPref storage
    /// </summary>
    private static void Save()
    {
        string primaryDataToSave = "";
        string secondaryDataToSave = "";
        foreach (Mapping mapping in keyMaps)
        {
            primaryDataToSave += "|" + mapping.PrimaryInput.ToString();
            secondaryDataToSave += "|" + mapping.SecondryInput.ToString();
        }
        MyPrefs.SetString(StringPref.primaryInputs, primaryDataToSave.Substring(1));
        MyPrefs.SetString(StringPref.secondryInputs, primaryDataToSave.Substring(1));
    }

    /// <summary>
    /// Overwrites the input PlayerPref with the default values and re-initializes the dictionary of inputs
    /// </summary>
    public static void ResetAllKeys()
    {
        MyPrefs.SetString(StringPref.primaryInputs, defaultPrimaryKeys);
        MyPrefs.SetString(StringPref.secondryInputs, defaultSecondryKeys);
        Save();
        InitializeMaps();
    }

    /// <summary>
    /// Finds an input by it's name and overwrites it's KeyCode
    /// </summary>
    /// <param name="input">The name of the input</param>
    /// <param name="primaryKey">The new KeyCode for that input</param>
    public static void SetKeyMap(string input, KeyCode? primaryKey = null, KeyCode? secondryKey = null)
    {
        if (keyMaps.Where(a => a.Name == input).Count() != 1)
            throw new ArgumentException("Invalid KeyMap in SetKeyMap: " + input);
        if (primaryKey != null)
            keyMaps.FirstOrDefault(a => a.Name == input).PrimaryInput = (KeyCode)primaryKey;
        if (secondryKey != null)
            keyMaps.FirstOrDefault(a => a.Name == input).SecondryInput = (KeyCode)secondryKey;
        Save();
    }

    /// <summary>
    /// Finds an input by it's name and returns the paired KeyCode
    /// </summary>
    /// <param name="input">The name of the input</param>
    /// <returns>The KeyCode assigned to that name</returns>
    public static Mapping GetKeyMap(string input)
    {
        if (keyMaps.Where(a => a.Name == input).Count() != 1)
            throw new ArgumentException("Invalid KeyMap in GetKeyMap: " + input);
        return keyMaps.FirstOrDefault(a => a.Name == input);
    }

    /// <summary>
    /// Finds an input by it's name and returns the paired KeyCode
    /// </summary>
    /// <param name="input">The name of the input</param>
    /// <returns>The KeyCode assigned to that name</returns>
    public static Mapping GetKeyMap(Control control)
    {
        string input = control.ToString();
        return keyMaps.FirstOrDefault(a => a.Name == input);
    }

    /// <summary>
    /// Finds an input by it's name and returns if this is the current update where it first pressed down
    /// </summary>
    /// <param name="input">The name of the input</param>
    /// <returns>True if the input is pressed down but wasn't in the previous update</returns>
    public static bool GetButtonDown(string input)
    {
        if (keyMaps.Where(a => a.Name == input).Count() != 1)
            throw new ArgumentException("Invalid KeyMap in GetButtonDown: " + input);
        Mapping map = keyMaps.FirstOrDefault(a => a.Name == input);
        if (map.SecondryInput != null)
            return Input.GetKeyDown(map.PrimaryInput) || Input.GetKeyDown((KeyCode)map.SecondryInput);
        else
            return Input.GetKeyDown(map.PrimaryInput);
    }

    /// <summary>
    /// Finds an input by it's name and returns if this is the current update where it first pressed down
    /// </summary>
    /// <param name="input">The name of the input</param>
    /// <returns>True if the input is pressed down but wasn't in the previous update</returns>
    public static bool GetButtonDown(Control control)
    {
        string input = control.ToString();
        Mapping map = keyMaps.FirstOrDefault(a => a.Name == input);
        if (map.SecondryInput != null)
            return Input.GetKeyDown(map.PrimaryInput) || Input.GetKeyDown((KeyCode)map.SecondryInput);
        else
            return Input.GetKeyDown(map.PrimaryInput);
    }

    /// <summary>
    /// Finds an input by it's name and returns if this is the current update where it first released
    /// </summary>
    /// <param name="input">The name of the input</param>
    /// <returns>True if the input is not pressed down but was in the previous update</returns>
    public static bool GetButtonUp(string input)
    {
        if (keyMaps.Where(a => a.Name == input).Count() != 1)
            throw new ArgumentException("Invalid KeyMap in GetButtonUp: " + input);
        Mapping map = keyMaps.FirstOrDefault(a => a.Name == input);
        if (map.SecondryInput != null)
            return Input.GetKeyUp(map.PrimaryInput) || Input.GetKeyUp((KeyCode)map.SecondryInput);
        else
            return Input.GetKeyUp(map.PrimaryInput);
    }

    /// <summary>
    /// Finds an input by it's name and returns if this is the current update where it first released
    /// </summary>
    /// <param name="input">The name of the input</param>
    /// <returns>True if the input is not pressed down but was in the previous update</returns>
    public static bool GetButtonUp(Control control)
    {
        string input = control.ToString();
        Mapping map = keyMaps.FirstOrDefault(a => a.Name == input);
        if (map.SecondryInput != null)
            return Input.GetKeyUp(map.PrimaryInput) || Input.GetKeyUp((KeyCode)map.SecondryInput);
        else
            return Input.GetKeyUp(map.PrimaryInput);
    }

    /// <summary>
    /// Finds an input by it's name and returns if it is pressed down or not
    /// </summary>
    /// <param name="input">The name of the input</param>
    /// <returns>True if the input is pressed down irrelevant of the last update</returns>
    public static bool GetButton(string input)
    {
        if (keyMaps.Where(a => a.Name == input).Count() != 1)
            throw new ArgumentException("Invalid KeyMap in GetButton: " + input);
        Mapping map = keyMaps.FirstOrDefault(a => a.Name == input);
        if (map.SecondryInput != null)
            return Input.GetKey(map.PrimaryInput) || Input.GetKey((KeyCode)map.SecondryInput);
        else
            return Input.GetKey(map.PrimaryInput);
    }

    /// <summary>
    /// Finds an input by it's name and returns if it is pressed down or not
    /// </summary>
    /// <param name="input">The name of the input</param>
    /// <returns>True if the input is pressed down irrelevant of the last update</returns>
    public static bool GetButton(Control control)
    {
        string input = control.ToString();
        Mapping map = keyMaps.FirstOrDefault(a => a.Name == input);
        if (map.SecondryInput != null)
            return Input.GetKey(map.PrimaryInput) || Input.GetKey((KeyCode)map.SecondryInput);
        else
            return Input.GetKey(map.PrimaryInput);
    }
}