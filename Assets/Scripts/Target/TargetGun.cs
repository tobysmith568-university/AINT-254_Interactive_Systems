using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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
    Damage playerDamage;

    [SerializeField]
    SphereCollider listener;

    [SerializeField]
    AudioSource fireSource;
    [SerializeField]
    AudioClip[] bulletWizzes;
    [SerializeField]
    AudioMixerGroup mixerGroup;

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
        int upperOdd = 3;
        fireSource.Play();
        flame.Play();
        ammo--;
        Vector3 direction = (player.position - thisTransform.position).normalized;
        if (!isFirstShot)
        {
            direction.x += Random.Range(-0.0075f, 0.0075f);
            direction.y += Random.Range(-0.0075f, 0.0075f);
            direction.z += Random.Range(-0.0075f, 0.0075f);
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
                upperOdd = 0;
                Debug.DrawRay(raycastHit.point, -dir.normalized * mag, Color.red, 10000f);
                playerDamage.Shot();
            }
            else
            {
                //Debug.DrawRay(raycastHit.point, -dir.normalized * mag, Color.blue, 10000f);
            }
        }

        if (Random.Range(0, upperOdd) == 1)
        {
            AudioClip clip = bulletWizzes[Random.Range(0, bulletWizzes.Length - 1)];
            Vector3 position = player.position;
            position.x += Random.Range(-2f, 2f);
            position.y += Random.Range(-2f, 2f);
            position.z += Random.Range(-2f, 2f);
            
            BulletWizz(clip, position);
        }

        if (ammo == 0)
            lockedOn = false;
    }

    private AudioSource BulletWizz(AudioClip clip, Vector3 pos)
    {
        GameObject tempGO = new GameObject("TempAudio");
        tempGO.transform.position = pos;
        AudioSource aSource = tempGO.AddComponent<AudioSource>();
        aSource.clip = clip;
        aSource.minDistance = 0;
        aSource.maxDistance = 25;
        aSource.outputAudioMixerGroup = mixerGroup;

        aSource.Play();
        Destroy(tempGO, clip.length);
        return aSource;
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