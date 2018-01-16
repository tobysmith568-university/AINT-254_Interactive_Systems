using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    PlayerGun thisParent;
    Vector3 previousPos;
    Vector3 thisPos;
    Vector3 stepDirection;
    float stepSize;
    float speed = 800f;

    [SerializeField]
    LayerMask killLayerMask;
    [SerializeField]
    LayerMask alertOnlyLayerMask;

    int counter;

    void Start()
    {
        Destroy(gameObject, 10);
    }

    void FixedUpdate()
    {
        //Calculate if the bullet has moved more than 10cm
        thisPos = transform.position;
        stepDirection = GetComponent<Rigidbody>().velocity.normalized;
        stepSize = (thisPos - previousPos).magnitude;
        if (stepSize > 0.1)
        {
            RaycastHit alertRaycastHit;

            //If so, fire as many rays as it takes to clear out and trigger any listener colliders
            while (Physics.Raycast(previousPos, stepDirection, out alertRaycastHit, stepSize, alertOnlyLayerMask))
                alertRaycastHit.transform.GetComponentInChildren<TargetGun>().LockOn();

            //Fire one damage ray
            if (Physics.Raycast(previousPos, stepDirection, out GameController.raycastHit, stepSize, killLayerMask))
            {
                //If it hits a collider with the target tag, tell it it's been hit
                if (GameController.raycastHit.transform.tag.Split('|')[0] == "Target")
                    thisParent.TargetHit();

                Destroy(gameObject);
            }
            else
                previousPos = thisPos;
        }
    }

    /// <summary>
    /// Shoots the bullet from a given parent
    /// </summary>
    /// <param name="parent">Where to shoot the bullet from</param>
    public void Shoot(PlayerGun parent)
    {
        thisParent = parent;
        previousPos = transform.position;
        GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.VelocityChange);
    }
}
