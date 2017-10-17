using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGun : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;

    [SerializeField]
    PlayerMovement playerMovement;

    Animator gunAnimator;
    [SerializeField]
    Animator handAnimator;

    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    Camera scopeCamera;
    Camera currentCamera;

    [SerializeField]
    ParticleSystem flame;

    [SerializeField]
    Text scoreMessage;

    RaycastHit raycastHit;

    bool isScoped;

    int sinceScope = 0;
    int sinceKill = 0;
    float shootdistance = 0;

    private void Start()
    {
        Cursor.visible = false;
        gunAnimator = GetComponent<Animator>();
        InvokeRepeating("IncrementSinceKill", 0.01f, 0.01f);
    }

    private void Update()
    {
        //Firing
        if (MyInput.GetButtonDown("Shoot"))
        {
            handAnimator.SetTrigger("Shoot");
            currentCamera = (isScoped) ? scopeCamera : mainCamera;
            if (Physics.Raycast(currentCamera.transform.position, currentCamera.transform.forward, out raycastHit))
            {
                Hit();
            }
            flame.Play();
            if (isScoped)
                Invoke("ToggleScoped", 0.10f);
        }

        //Scoping
        if (MyInput.GetButtonDown("Scope"))
            ToggleScoped();
    }

    /// <summary>
    /// Ran when the gun is fired and hits an object
    /// </summary>
    private void Hit()
    {
        if (raycastHit.transform.tag == "Target")
        {
            //Cancel invokes
            CancelInvoke("HideScoreMessage");
            CancelInvoke("IncrementSinceKill");

            //Tell the target it has been hit
            raycastHit.transform.SendMessage("BeenShot");

            //Find and show the kill stats
            shootdistance = Vector3.Distance(playerTransform.position, raycastHit.transform.position);
            SetScoreMessage("Scoped time: " + sinceScope +
                          "\nHit distance: " + Mathf.Round(shootdistance) +
                          "\nHit interval: " + sinceKill +
                          "\nHit time: " + Scoring.Time.ToString("n2"));

            //Set up next kill stats
            Invoke("HideScoreMessage", 2f);
            sinceKill = 0;
            InvokeRepeating("IncrementSinceKill", 0.01f, 0.01f);
        }
    }

    public void ToggleScoped()
    {
        playerMovement.SendMessage("SetScoped", isScoped);
        gunAnimator.SetTrigger((isScoped) ? "ScopeOut" : "ScopeUp");
        isScoped = !isScoped;

        if (isScoped)
            InvokeRepeating("IncrementSinceScope", 0.01f, 0.01f);
        else
        {
            CancelInvoke("IncrementSinceScope");
            sinceScope = 0;
        }
    }

#region Methods for Invoking

    private void IncrementSinceScope()
    {
        sinceScope++;
    }

    private void IncrementSinceKill()
    {
        sinceKill++;
    }

    private void SetScoreMessage(string message)
    {
        scoreMessage.text = message;
    }

    private void HideScoreMessage()
    {
        scoreMessage.text = "";
    }

#endregion
}
