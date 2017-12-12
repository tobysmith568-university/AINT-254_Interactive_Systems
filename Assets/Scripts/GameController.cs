using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// THIS SCRIPT IS ADDED TO THE EVENT LISTENER GAMEOBJECT IN THE SCENE!
/// </summary>
public class GameController : MonoBehaviour
{
    private static GameController singleton;

    private void Start()
    {
        singleton = this;

        //Game setup
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Scoring.FullReset();

        //Updating the UI inside the player's scope
        remainingTargets = targets.ToList();
    }

    #region Updating the UI inside the player's scope

    [SerializeField]
    Transform[] targets;
    List<Transform> remainingTargets;
    [SerializeField]
    Text targetsRemaining;

    public static void UpdateScopeUI()
    {
        singleton._UpdateScopeUI();
    }

    private void _UpdateScopeUI()
    {
        targetsRemaining.text = "Targets Remaining: " + remainingTargets.Count();
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
}