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

    [SerializeField]
    SphereCollider listener;

    bool isFirstShot;

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
        Vector3 direction = (player.position - thisTransform.position).normalized;
        if (!isFirstShot)
        {
            direction.x += Random.Range(-0.0125f, 0.0125f);
            direction.y += Random.Range(-0.0125f, 0.0125f);
            direction.z += Random.Range(-0.0125f, 0.0125f);
        }
        else
            isFirstShot = false;
        Debug.DrawRay(thisTransform.position, direction * 500, Color.blue, 10000f);
        if (Physics.Raycast(thisTransform.position, direction, out raycastHit, 500f, notGun))
        {
            Vector3 dir = raycastHit.point - thisTransform.position;
            float mag = dir.magnitude;

            if (raycastHit.transform.name == "PlayerMain")
            {
                Debug.DrawRay(raycastHit.point, -dir.normalized * mag, Color.red, 10000f);
                playerMovement.Shot();
            }
            else
            {
                //Debug.DrawRay(raycastHit.point, -dir.normalized * mag, Color.blue, 10000f);
            }
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
        isFirstShot = true;
        thisAnimator.SetBool("isShooting", true);
    }

    public void LockOn()
    {
        lockedOn = true;
        listener.enabled = false;
        Invoke("EnableListener", 0.2f);
    }

    private void EnableListener()
    {
        listener.enabled = true;
    }
}