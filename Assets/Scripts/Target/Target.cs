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

    MeshRenderer mesh;

    public bool beenShot;

    void Awake()
    {
    }

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        if (gameObject.name.Contains("Person Target"))
            GetComponent<Animator>().SetInteger("ID", int.Parse(gameObject.name.Split('(')[1].Split(')')[0]));

        GameController.SetTarget(gameObject, true);
    }

    /// <summary>
    /// Called by the player when they have shot this target
    /// </summary>
    public void BeenShot()
    {
        beenShot = true;
        smoke.Play();
        Invoke("Hide", 0.2f);
        Invoke("Kill", 0.5f);

        //Spawn in the new targets for this target
        for (int i = 0; i < newTargets.Length; i++)
        {
            newTargets[i].SetActive(true);
        }

        GameController.SetTarget(gameObject, false);
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
