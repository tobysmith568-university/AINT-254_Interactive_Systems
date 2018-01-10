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
    Reload,
    Pause
}

public class MyInput : MonoBehaviour
{
    public static List<Mapping> keyMaps;

    static string[,] defaultKeys =
        {
            { "Mouse0", "null" }, { "Mouse1", "null" },  { "W", "UpArrow" },      { "S", "DownArrow" },
            { "A", "LeftArrow" }, { "D", "RightArrow" }, { "LeftShift", "null" }, { "LeftControl", "null" },
            { "Space", "null" },  { "R", "null" },       { "Escape", "P" }
        };

    static MyInput()
    {
        if (MyPrefs.Exists(MyPrefs.Prefs.KeyMappings))
            keyMaps = MyPrefs.KeyMappings.ToList();
        else
            ResetMappings();
    }

    /// <summary>
    /// This method takes the saved PlayerPref for the inputs and sets up
    /// dictionary entries in the key map dictionary for each of them
    /// </summary>
    public static void ResetMappings()
    {
        keyMaps = new List<Mapping>();
        for (int i = 0; i < Enum.GetValues(typeof(Control)).Length; i++)
        {
            if (defaultKeys[i, 1] == "null")
                keyMaps.Add(new Mapping(
                    ((Control)Enum.GetValues(typeof(Control)).GetValue(i)).ToString(),
                    (KeyCode)Enum.Parse(typeof(KeyCode), defaultKeys[i, 0])));
            else
                keyMaps.Add(new Mapping(
                    ((Control)Enum.GetValues(typeof(Control)).GetValue(i)).ToString(),
                    (KeyCode)Enum.Parse(typeof(KeyCode), defaultKeys[i, 0]),
                    (KeyCode)Enum.Parse(typeof(KeyCode), defaultKeys[i, 1])));
        }
        MyPrefs.KeyMappings = keyMaps.ToArray();
    }

    /// <summary>
    /// Finds an input by it's name and overwrites it's KeyCode
    /// </summary>
    /// <param name="input">The name of the input</param>
    /// <param name="primaryKey">The new KeyCode for that input</param>
    public static void SetKeyMap(string input, KeyCode? primaryKey = null, KeyCode? secondaryKey = null)
    {
        if (keyMaps.Where(a => a.Name == input).Count() != 1)
            throw new ArgumentException("Invalid KeyMap in SetKeyMap: " + input);
        if (primaryKey != null)
            keyMaps.FirstOrDefault(a => a.Name == input).PrimaryInput = (KeyCode)primaryKey;
        if (secondaryKey != null)
            keyMaps.FirstOrDefault(a => a.Name == input).SecondaryInput = (KeyCode)secondaryKey;
        MyPrefs.KeyMappings = keyMaps.ToArray();
    }

    /// <summary>
    /// Finds an input by it's Control and overwrites it's KeyCode
    /// </summary>
    /// <param name="control">The Control of the input</param>
    /// <param name="primaryKey">The new KeyCode for that input</param>
    public static void SetKeyMap(Control control, KeyCode? primaryKey = null, KeyCode? secondaryKey = null)
    {
        if (primaryKey != null)
            keyMaps.FirstOrDefault(a => a.Name == control.ToString()).PrimaryInput = (KeyCode)primaryKey;
        if (secondaryKey != null)
            keyMaps.FirstOrDefault(a => a.Name == control.ToString()).SecondaryInput = (KeyCode)secondaryKey;
        MyPrefs.KeyMappings = keyMaps.ToArray();
    }

    /// <summary>
    /// Removes the optional secondary mapping of an input
    /// </summary>
    /// <param name="control">The Control of the input</param>
    public static void RemoveSecondMapping(Control control)
    {
        keyMaps.FirstOrDefault(a => a.Name == control.ToString()).SecondaryInput = null;
        MyPrefs.KeyMappings = keyMaps.ToArray();
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
    /// Finds an input by it's Control and returns the paired KeyCode
    /// </summary>
    /// <param name="control">The Control of the input</param>
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
        if (map.SecondaryInput != null)
            return Input.GetKeyDown(map.PrimaryInput) || Input.GetKeyDown((KeyCode)map.SecondaryInput);
        else
            return Input.GetKeyDown(map.PrimaryInput);
    }

    /// <summary>
    /// Finds an input by it's Control and returns if this is the current update where it first pressed down
    /// </summary>
    /// <param name="control">The Control of the input</param>
    /// <returns>True if the input is pressed down but wasn't in the previous update</returns>
    public static bool GetButtonDown(Control control)
    {
        string input = control.ToString();
        Mapping map = keyMaps.FirstOrDefault(a => a.Name == input);
        if (map.SecondaryInput != null)
            return Input.GetKeyDown(map.PrimaryInput) || Input.GetKeyDown((KeyCode)map.SecondaryInput);
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
        if (map.SecondaryInput != null)
            return Input.GetKeyUp(map.PrimaryInput) || Input.GetKeyUp((KeyCode)map.SecondaryInput);
        else
            return Input.GetKeyUp(map.PrimaryInput);
    }

    /// <summary>
    /// Finds an input by it's Control and returns if this is the current update where it first released
    /// </summary>
    /// <param name="control">The Control of the input</param>
    /// <returns>True if the input is not pressed down but was in the previous update</returns>
    public static bool GetButtonUp(Control control)
    {
        string input = control.ToString();
        Mapping map = keyMaps.FirstOrDefault(a => a.Name == input);
        if (map.SecondaryInput != null)
            return Input.GetKeyUp(map.PrimaryInput) || Input.GetKeyUp((KeyCode)map.SecondaryInput);
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
        if (map.SecondaryInput != null)
            return Input.GetKey(map.PrimaryInput) || Input.GetKey((KeyCode)map.SecondaryInput);
        else
            return Input.GetKey(map.PrimaryInput);
    }

    /// <summary>
    /// Finds an input by it's Control and returns if it is pressed down or not
    /// </summary>
    /// <param name="control">The Control of the input</param>
    /// <returns>True if the input is pressed down irrelevant of the last update</returns>
    public static bool GetButton(Control control)
    {
        string input = control.ToString();
        Mapping map = keyMaps.FirstOrDefault(a => a.Name == input);
        if (map.SecondaryInput != null)
            return Input.GetKey(map.PrimaryInput) || Input.GetKey((KeyCode)map.SecondaryInput);
        else
            return Input.GetKey(map.PrimaryInput);
    }
}