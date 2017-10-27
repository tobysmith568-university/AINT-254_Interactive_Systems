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

    [SerializeField]
    Image crosshair;

    [SerializeField]
    Text targetsRemaining;
    [SerializeField]
    Transform targets;

    RaycastHit raycastHit;

    bool isScoped;
    bool canFire = true;
    bool justFired = false;

    int sinceScope = 0;
    int sinceKill = 0;
    float shootDistance = 0;
    
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gunAnimator = GetComponent<Animator>();
        InvokeRepeating("IncrementSinceKill", 0.01f, 0.01f);
        crosshair.color = new Color(MyPrefs.GetFloat(FloatPref.CrosshairRed),
                                    MyPrefs.GetFloat(FloatPref.CrosshairGreen),
                                    MyPrefs.GetFloat(FloatPref.CrosshairBlue));
    }

    private void Update()
    {
        //Firing
        if (MyInput.GetButtonDown("Shoot") && canFire)
        {
            handAnimator.SetTrigger("Shoot");
            currentCamera = (isScoped) ? scopeCamera : mainCamera;
            if (Physics.Raycast(currentCamera.transform.position, currentCamera.transform.forward, out raycastHit))
            {
                Hit();
            }
            flame.Play();
            if (isScoped)
                justFired = true;
        }

        //Scoping
        if (justFired && MyInput.GetButtonUp(Control.Shoot))
        {
            Invoke("ToggleScoped", 0.10f);
            justFired = false;
        }

        if (MyInput.GetButtonDown("Scope"))
            ToggleScoped();

        //Scope UI
        targetsRemaining.text = "Targets Remaining: " + targets.childCount;
    }

    /// <summary>
    /// Ran when the gun is fired and hits an object
    /// </summary>
    private void Hit()
    {
        int noscopeBonus = 0, quickscopeBonus = 0, longshotBonus = 0, chainkillBonus = 0;
        if (raycastHit.transform.tag == "Target")
        {
            //Cancel invokes
            CancelInvoke("HideScoreMessage");
            CancelInvoke("IncrementSinceKill");

            //Tell the target it has been hit
            raycastHit.transform.GetComponent<Target>().BeenShot();

            //Find the kill stats
            if (sinceScope < 40)
                noscopeBonus = 30;
            else if (sinceScope < 50)
                quickscopeBonus = 50;
            else if (sinceScope < 60)
                quickscopeBonus = 40;
            else if (sinceScope < 70)
                quickscopeBonus = 30;
            else if (sinceScope < 80)
                quickscopeBonus = 20;
            else if (sinceScope < 90)
                quickscopeBonus = 10;

            shootDistance = Vector3.Distance(playerTransform.position, raycastHit.transform.position);
            if (shootDistance > 50)
                longshotBonus = (int)shootDistance - 50;
            
            if (sinceKill < 150)
                chainkillBonus = 30;

            string message = "Kill: 100";
            if (noscopeBonus != 0)
                message += "\nNo-scope Bonus: " + noscopeBonus;
            if (quickscopeBonus != 0)
                message += "\nQuick-scope Bonus: " + quickscopeBonus;
            if (longshotBonus != 0)
                message += "\nLongshot Bonus: " + longshotBonus;
            if (chainkillBonus != 0)
                message += "\nChainkill Bonus: " + chainkillBonus;

            //Show the kill stats
            SetScoreMessage(message);

            //Increment the player score
            Scoring.AddScore(100 + noscopeBonus + quickscopeBonus + longshotBonus + chainkillBonus);

            //Set up next kill stats
            Invoke("HideScoreMessage", 2f);
            sinceKill = 0;
            InvokeRepeating("IncrementSinceKill", 0.01f, 0.01f);
        }
    }

    public void ToggleScoped()
    {
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

    public void CantFire()
    {
        canFire = false;
    }

    public void CanFire()
    {
        canFire = true;
    }

    public void InFront()
    {
        playerMovement.SetScoped(true);
        //m.material.renderQueue = 500;
        //Debug.Log("Set to: " + m.material.renderQueue);
    }

    public void NotInFront()
    {
        playerMovement.SetScoped(false);
        //m.material.renderQueue = defaultQueue;
        //Debug.Log("Set to: " + m.material.renderQueue);
    }

    #endregion
}
