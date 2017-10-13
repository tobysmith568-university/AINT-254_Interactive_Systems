using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
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

    RaycastHit raycastHit;

    bool isScoped;

    private void Start()
    {
        Cursor.visible = false;
        gunAnimator = GetComponent<Animator>();
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
                if (raycastHit.transform.tag == "Target")
                    raycastHit.transform.SendMessage("BeenShot");
            }            
            if (isScoped)
                Invoke("ToggleScoped", 0.10f);
        }

        //Scoping
        if (MyInput.GetButtonDown("Scope"))
            ToggleScoped();
    }

    public void ToggleScoped()
    {
        playerMovement.SendMessage("SetScoped", isScoped);
        gunAnimator.SetTrigger((isScoped) ? "ScopeOut" : "ScopeUp");
        isScoped = !isScoped;
    }
}
