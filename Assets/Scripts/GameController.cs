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
            SceneManager.LoadScene(1, LoadSceneMode.Single);
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
    Text[] labelMessages;
    [SerializeField]
    Text[] scoreMessages;
    [SerializeField]
    AudioSource beepSource;

    Coroutine[] coroutines = new Coroutine[6];

    public static void SendScoreMessage(int killScore = 100, int noscopeBonus = 0, int quickscopeBonus = 0, int longshotBonus = 0, int chainkillBonus = 0, int headshotBonus = 0)
    {
        singleton.StartCoroutine(singleton._SendScoreMessage(killScore, noscopeBonus, quickscopeBonus, longshotBonus, chainkillBonus, headshotBonus));
    }

    IEnumerator _SendScoreMessage(int killScore = 100, int noscopeBonus = 0, int quickscopeBonus = 0, int longshotBonus = 0, int chainkillBonus = 0, int headshotBonus = 0)
    {
        int index = 0;

        if (killScore != 0)
        {
            AddScore(ref index, "Kill:", killScore);
            yield return new WaitForSeconds(0.1f);
        }
        if (noscopeBonus != 0)
        {
            AddScore(ref index, "No-scope Bonus:", noscopeBonus);
            yield return new WaitForSeconds(0.1f);
        }
        if (quickscopeBonus != 0)
        {
            AddScore(ref index, "Quick-scope Bonus:", quickscopeBonus);
            yield return new WaitForSeconds(0.1f);
        }
        if (longshotBonus != 0)
        {
            AddScore(ref index, "Longshot Bonus:", longshotBonus);
            yield return new WaitForSeconds(0.1f);
        }
        if (chainkillBonus != 0)
        {
            AddScore(ref index, "Chainkill Bonus:", chainkillBonus);
            yield return new WaitForSeconds(0.1f);
        }        
        if (headshotBonus != 0)
            AddScore(ref index, "Headshot Bonus:", headshotBonus);
    }

    private void AddScore(ref int index, string label, int score)
    {
        if (coroutines[index] != null)
            StopCoroutine(coroutines[index]);
        labelMessages[index].text = label;
        scoreMessages[index].text = "" + score;
        coroutines[index] = StartCoroutine(HideMessage(index, 2));
        beepSource.Play();
        index++;
    }

    IEnumerator HideMessage(int message, int delay)
    {
        yield return new WaitForSeconds(delay);

        labelMessages[message].text = "";
        scoreMessages[message].text = "";
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

        string name = MyPrefs.LastPlay.Name;
        MyPrefs.LastPlay = new GameScore(name, Scoring.Score, (int)System.TimeSpan.FromSeconds(Scoring.Time).TotalMilliseconds);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }

    public static void Killed()
    {
        SceneManager.LoadScene(4);
    }

    #endregion
}