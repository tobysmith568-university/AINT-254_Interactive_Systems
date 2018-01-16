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
        thisPos = transform.position;
        stepDirection = GetComponent<Rigidbody>().velocity.normalized;
        stepSize = (thisPos - previousPos).magnitude;
        if (stepSize > 0.1)
        {
            RaycastHit alertRaycastHit;
            while (Physics.Raycast(previousPos, stepDirection, out alertRaycastHit, stepSize, alertOnlyLayerMask))
                alertRaycastHit.transform.GetComponentInChildren<TargetGun>().LockOn();

            if (Physics.Raycast(previousPos, stepDirection, out GameController.raycastHit, stepSize, killLayerMask))
            {
                Debug.Log(GameController.raycastHit.transform.name);
                if (GameController.raycastHit.transform.tag.Split('|')[0] == "Target")
                    thisParent.TargetHit();

                Destroy(gameObject);
            }
            else
                previousPos = thisPos;
        }
    }

    public void Shoot(PlayerGun parent)
    {
        thisParent = parent;
        previousPos = transform.position;
        GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.VelocityChange);
    }
}
