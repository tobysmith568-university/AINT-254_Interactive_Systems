using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    ParticleSystem smoke;

    [SerializeField]
    GameObject[] newTargets;

    public void BeenShot()
    {
        smoke.Play();
        Invoke("Hide", 0.2f);
        Invoke("Kill", 0.5f);
        foreach (GameObject target in newTargets)
        {
            target.SetActive(true);
        }
    }

    void Hide()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}
