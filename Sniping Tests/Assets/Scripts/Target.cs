using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    ParticleSystem smoke;

    void BeenShot()
    {
        smoke.Play();
        Invoke("Hide", 0.2f);
        Invoke("Kill", 0.5f);
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
