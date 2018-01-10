using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

/// <summary>
/// THIS SCRIPT IS ADDED TO THE EVENT LISTENER GAMEOBJECT IN THE SCENE!
/// </summary>
public class GameController : MonoBehaviour
{
    private static GameController singleton;

    private void Awake()
    {
        singleton = this;
        activeTargets.Clear();
    }

    private void Start()
    {
        //Game setup
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Scoring.FullReset();
    }

    private void Update()
    {
        //Game quitting
        if (MyInput.GetButtonDown(Control.Pause))
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }

    #region Updating the UI inside the player's scope

    [SerializeField]
    Transform[] targets;
    [SerializeField]
    Text targetsRemaining;

    private void UpdateScopeUI()
    {
        targetsRemaining.text = "Targets Remaining: " + activeTargets.Count(a => a.Value == true);
    }

    #endregion
    #region Setting the points breakdown message after a kill

    [SerializeField]
    Text scoreMessage;

    public static void SendScoreMessage(int killScore = 100, int noscopeBonus = 0, int quickscopeBonus = 0, int longshotBonus = 0, int chainkillBonus = 0, int headshotBonus = 0)
    {
        singleton._SendScoreMessage(killScore, noscopeBonus, quickscopeBonus, longshotBonus, chainkillBonus, headshotBonus);
    }

    private void _SendScoreMessage(int killScore = 100, int noscopeBonus = 0, int quickscopeBonus = 0, int longshotBonus = 0, int chainkillBonus = 0, int headshotBonus = 0)
    {
        string message = "Kill: " + killScore;
        if (noscopeBonus != 0)
            message += "\nNo-scope Bonus: " + noscopeBonus;
        if (quickscopeBonus != 0)
            message += "\nQuick-scope Bonus: " + quickscopeBonus;
        if (longshotBonus != 0)
            message += "\nLongshot Bonus: " + longshotBonus;
        if (chainkillBonus != 0)
            message += "\nChainkill Bonus: " + chainkillBonus;
        if (headshotBonus != 0)
            message += "\nHeadshot Bonus: " + headshotBonus;

        scoreMessage.text = message;
        Invoke("HideScoreMessage", 2f);
    }

    /// <summary>
    /// Invoked to hide the score message
    /// </summary>
    void HideScoreMessage()
    {
        scoreMessage.text = "";
    }

    #endregion
    #region Detecting when the game should end

    private static Dictionary<GameObject, bool> activeTargets = new Dictionary<GameObject, bool>();
    
    /// <summary>
    /// Called by a Target on Start() as well as when it is shot
    /// </summary>
    /// <param name="target">The target which has been shot</param>
    /// <param name="state">The new state of the target - ie, is it active or not</param>
    public static void SetTarget(GameObject target, bool state)
    {
        if (activeTargets.ContainsKey(target))
            activeTargets[target] = state;
        else
            activeTargets.Add(target, state);

        singleton.UpdateScopeUI();

        if (activeTargets.Count(a => a.Value == true) > 0)
            return;

        Debug.Log("Game Ended");

        MyPrefs.LastPlay = new GameScore(MyPrefs.LastPlay.Name, Scoring.Score, (int)System.TimeSpan.FromSeconds(Scoring.Time).TotalMilliseconds);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }

    public static void Killed()
    {
        SceneManager.LoadScene(6);
    }

    #endregion
}