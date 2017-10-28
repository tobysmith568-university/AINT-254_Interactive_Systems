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
    }

    public void BeenShot()
    {
        smoke.Play();
        Invoke("Hide", 0.2f);
        Invoke("Kill", 0.5f);
        foreach (GameObject target in newTargets)
        {
            target.SetActive(true);
            target.GetComponent<Transform>().SetParent(parent);
        }

        //End of game
        if (parent.childCount == 1)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(3, LoadSceneMode.Single);
        }
    }

    void Hide()
    {
        if(mesh != null)
            mesh.enabled = false;
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
