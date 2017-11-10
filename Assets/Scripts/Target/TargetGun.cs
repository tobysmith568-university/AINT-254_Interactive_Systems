using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGun : MonoBehaviour
{
    [SerializeField]
    bool lockedOn;

    [SerializeField]
    Transform hand;
    [SerializeField]
    Transform player;

    [SerializeField]
    Animator targetAnimator;
    Animator thisAnimator;

    [SerializeField]
    ParticleSystem flame;

    bool isRunning;

    int ammo;

    [SerializeField]
    LayerMask notGun;

    [SerializeField]
    Transform eyes;

    Transform thisTransform;
    Quaternion targetRotation;
    Quaternion defaultRotation;

    RaycastHit raycastHit;

    [SerializeField]
    PlayerMovement playerMovement;

    void Start()
    {
        thisAnimator = GetComponent<Animator>();
        thisTransform = GetComponent<Transform>();
    }

    void Update()
    {
        //If the lockedOn code needs to be stopped
        if (!lockedOn && isRunning)
        {
            thisAnimator.SetBool("isShooting", false);
            StopCoroutine(Look(targetRotation));
            Invoke("StopLooking", 0.5f);
            isRunning = false;
            Invoke("StartWalk", 2);
        }
        //Else if the lockedOn code needs to be started
        else if (lockedOn && !isRunning)
        {
            ammo = (Random.Range(0, 100) % 10) + 10;
            Debug.Log(ammo);
            defaultRotation = thisTransform.rotation;
            eyes.LookAt(player);
            targetRotation = eyes.rotation;
            //targetRotation = Quaternion.LookRotation(player.position - thisTransform.position);
            StartCoroutine(Look(targetRotation));
            isRunning = true;
            targetAnimator.enabled = false;
            Invoke("StartShooting", 1.5f);
        }
    }

    /// <summary>
    /// Makes the target move their gun to point in a given rotation
    /// </summary>
    /// <param name="endRotation">The direction their gun should point</param>
    /// <returns>Always returns 0</returns>
    IEnumerator Look(Quaternion endRotation)
    {
        float rate = 1.0f / 2f;
        for (float i = 0.0f; i < 1.0f; i += Time.deltaTime * rate)
        {
            i += Time.deltaTime * rate;
            hand.rotation = Quaternion.Lerp(transform.rotation, endRotation, i);
            yield return 0;
        }
    }

    /// <summary>
    /// Called by the target's shoot animation, this fires their gun
    /// </summary>
    void Shoot()
    {
        flame.Play();
        ammo--;
        Vector3 direction = thisTransform.forward;
        direction.x += Random.Range(-0.0125f, 0.0125f);
        direction.y += Random.Range(-0.0125f, 0.0125f);
        direction.z += Random.Range(-0.0125f, 0.0125f);
        Debug.DrawRay(thisTransform.position, direction * 500, Color.blue, 10000f);
        if (Physics.Raycast(thisTransform.position, direction, out raycastHit, 500f, notGun))
        {
            if (raycastHit.transform.name == "PlayerMain")
                playerMovement.Shot();
        }

        if (ammo == 0)
            lockedOn = false;
    }

    /// <summary>
    /// Invoked by the update method
    /// </summary>
    void StartWalk()
    {
        targetAnimator.enabled = true;
    }

    /// <summary>
    /// Invoked by the update method
    /// </summary>
    void StopLooking()
    {
        StartCoroutine(Look(defaultRotation));
    }

    /// <summary>
    /// Invoked by the update method
    /// </summary>
    void StartShooting()
    {
        thisAnimator.SetBool("isShooting", true);
    }
}