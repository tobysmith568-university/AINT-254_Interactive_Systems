using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Target : MonoBehaviour
{
    [SerializeField]
    ParticleSystem smoke;

    [SerializeField]
    GameObject[] newTargets;

    Transform parent;
    MeshRenderer mesh;

    void Start()
    {
        parent = GetComponent<Transform>().parent;
        mesh = GetComponent<MeshRenderer>();
        if (gameObject.name.Contains("Person Target"))
            GetComponent<Animator>().SetInteger("ID", int.Parse(gameObject.name.Split('(')[1].Split(')')[0]));
    }

    /// <summary>
    /// Called by the player when they have shot this target
    /// </summary>
    public void BeenShot()
    {
        smoke.Play();
        Invoke("Hide", 0.2f);
        Invoke("Kill", 0.5f);

        //Spawn in the new targets for this target
        foreach (GameObject target in newTargets)
        {
            target.SetActive(true);
            target.GetComponent<Transform>().SetParent(parent);
        }

        //Check if the game is over
        if (parent.childCount == 1)
        {
            MyPrefs.LastPlay = new GameScore(MyPrefs.LastPlay.Name, Scoring.Score, System.TimeSpan.FromSeconds(Scoring.Time));
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(3, LoadSceneMode.Single);
        }
    }

    /// <summary>
    /// Hides this target except for it's particle system
    /// </summary>
    void Hide()
    {
        //If this is a box target
        if(mesh != null)
            mesh.enabled = false;
        //Else if this is a player target
        else
        {
            foreach (Transform child in transform)
            {
                if (child.name != "Particle System")
                    child.gameObject.SetActive(false);
            }
        }
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}
