using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class Scope : MonoBehaviour
{
    [SerializeField]
    Transform scopeCamera;
    [SerializeField]
    Text distance;
    [SerializeField]
    LayerMask layermask;

    RaycastHit raycastHit;

    private void Update()
    {
        distance.text = "Distance: ";
        if (Physics.Raycast(scopeCamera.position, transform.up, out raycastHit, Mathf.Infinity, layermask))
            distance.text += raycastHit.distance.ToString("f1") + "m";
    }
}