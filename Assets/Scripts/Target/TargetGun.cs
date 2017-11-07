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
    float strength = 5;

    bool isRunning;

    Quaternion targetRotation;

    private void Start()
    {

    }

    void Update()
    {
        if (!lockedOn)
        {
            StopCoroutine(Look());
            isRunning = false;
        }
        else if (!isRunning)
        {
            StartCoroutine(Look());
            isRunning = true;
        }

    }

    IEnumerator Look()
    {
        float i = 0.0f;
        float rate = 1.0f / 2f;
        while (i < 1.0)
        {
            targetRotation = Quaternion.LookRotation(player.position - transform.position);
            i += Time.deltaTime * rate;
            hand.rotation = Quaternion.Lerp(transform.rotation, targetRotation, i);
            yield return 0;
        }
    }
}