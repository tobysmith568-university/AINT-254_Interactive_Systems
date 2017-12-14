using System;
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
    Image crosshair;
    
    RaycastHit raycastHit;
    [SerializeField]
    LayerMask alertOnly;
    [SerializeField]
    LayerMask anyButAlert;


    bool isScoped;
    bool canFire = true;
    bool justFired = false;

    int sinceScope = 0;
    int sinceKill = 0;
    float shootDistance = 0;

    [SerializeField]
    int magSize = 8;
    [SerializeField]
    Text ammoText;
    int ammoCount;
    public int AmmoCount
    {
        get { return ammoCount; }
        set
        {
            ammoCount = value;
            ammoText.text = "Ammo: " + value;
        }
    }
    bool queuedReload = false;

    void Start()
    {
        gunAnimator = GetComponent<Animator>();
        InvokeRepeating("IncrementSinceKill", 0.01f, 0.01f);
        crosshair.color = new Color(MyPrefs.CrosshairRed,
                                    MyPrefs.CrosshairGreen,
                                    MyPrefs.CrosshairBlue);
        AmmoCount = magSize;
    }

    void Update()
    {
        //Firing
        if (MyInput.GetButtonDown(Control.Shoot) && canFire && AmmoCount > 0)
        {
            handAnimator.SetTrigger("Shoot");
            currentCamera = (isScoped) ? scopeCamera : mainCamera;
            while (Physics.Raycast(currentCamera.transform.position, currentCamera.transform.forward, out raycastHit, Mathf.Infinity, alertOnly))
                Alert();
            if (Physics.Raycast(currentCamera.transform.position, currentCamera.transform.forward, out raycastHit, Mathf.Infinity, anyButAlert))
            {
                Debug.DrawRay(raycastHit.point, currentCamera.transform.position, Color.yellow, Mathf.Infinity, false);
                if (raycastHit.transform.tag.Split('|')[0] == "Target")
                    Hit();
            }

            flame.Play();
            if (isScoped)
                justFired = true;
            AmmoCount--;
            if (AmmoCount == 0 && !isScoped)
                Reload();
            else if (AmmoCount == 0 && isScoped)
                CantFire();
        }

        //Scoping
        if (justFired && MyInput.GetButtonUp(Control.Shoot))
        {
            Invoke("ToggleScoped", 0.10f);
            justFired = false;
        }

        if (MyInput.GetButtonDown("Scope") && canFire && AmmoCount > 0)
            ToggleScoped();

        //Reloading
        if (MyInput.GetButtonDown(Control.Reload) && canFire)
            Reload();

        //Scope UI
        GameController.UpdateScopeUI();
    }

    /// <summary>
    /// Ran when the gun is fired and hits a target
    /// </summary>
    void Hit()
    {
        int noscopeBonus = 0, quickscopeBonus = 0, longshotBonus = 0, chainkillBonus = 0, headshotBonus = 0;
        //Cancel invokes
        CancelInvoke("HideScoreMessage");
        CancelInvoke("IncrementSinceKill");

        //Find the no-scope stat
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

        //Find the long-shot stat
        shootDistance = Vector3.Distance(playerTransform.position, raycastHit.transform.position);
        if (shootDistance > 50)
            longshotBonus = (int)shootDistance - 50;

        //Find the chainkill stat
        if (sinceKill < 150)
            chainkillBonus = 30;

        //Find the headshot stat
        if (raycastHit.collider.tag == "Target|Head")
            headshotBonus = 25;
        
        //Show the messages to the player
        GameController.SendScoreMessage(100, noscopeBonus, quickscopeBonus, longshotBonus, chainkillBonus, headshotBonus);

        //Increment the player score
        Scoring.AddScore(100 + noscopeBonus + quickscopeBonus + longshotBonus + chainkillBonus + headshotBonus);

        //Set up next kill stats
        sinceKill = 0;
        InvokeRepeating("IncrementSinceKill", 0.01f, 0.01f);

        //Tell the target it has been hit
        raycastHit.transform.GetComponent<Target>().BeenShot();
    }

    /// <summary>
    /// Ran when the gun is fired and hits a listener
    /// </summary>
    void Alert()
    {
        raycastHit.transform.GetComponentInChildren<TargetGun>().LockOn();
    }

    /// <summary>
    /// Changes the state of the player's scope
    /// </summary>
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

    /// <summary>
    /// Called by either the shoot method or by a player input to reload the players gun
    /// </summary>
    void Reload()
    {
        CantFire();
        if (isScoped)
        {
            ToggleScoped();
            queuedReload = true;
        }
        else
            gunAnimator.SetTrigger("Reload");
    }

    #region Methods for Invoking

    /// <summary>
    /// Invoked to increase the time since the user scoped up
    /// </summary>
    void IncrementSinceScope()
    {
        sinceScope++;
    }

    /// <summary>
    /// Invoked to increase the time since the user killed
    /// </summary>
    void IncrementSinceKill()
    {
        sinceKill++;
    }

    /// <summary>
    /// Called to disable firing
    /// </summary>
    public void CantFire()
    {
        canFire = false;
    }

    /// <summary>
    /// Called to enable firing
    /// </summary>
    public void CanFire()
    {
        canFire = true;
    }
    
    /// <summary>
    /// Called when the gun is scoped out. If it scoped out because the player
    /// pressed reload then called a reload too
    /// </summary>
    public void ScopedOut()
    {
        if (AmmoCount == 0)
            Reload();
        if (queuedReload)
            Reload();
    }

    /// <summary>
    /// Called by the reload animation to set the ammo count to 0 when the magazine is
    /// removed from the gun
    /// </summary>
    public void ClearMag()
    {
        AmmoCount = 0;
    }

    /// <summary>
    /// Called by the reload animation when it is finished
    /// </summary>
    public void Reloaded()
    {
        CanFire();
        AmmoCount = magSize;
        if (queuedReload)
        {
            ToggleScoped();
            queuedReload = false;
        }
    }

    /* TEST METHODS: For keeping the gun on top of everything else */
    public void InFront()
    {
        playerMovement.SetScoped(true);
        //m.material.renderQueue = 500;
    }

    public void NotInFront()
    {
        playerMovement.SetScoped(false);
        //m.material.renderQueue = defaultQueue;
    }

    /* END TEST METHODS */
    #endregion
}
