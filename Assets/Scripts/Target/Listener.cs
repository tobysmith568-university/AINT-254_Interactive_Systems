using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listener : MonoBehaviour
{
    [SerializeField]
    public TargetGun targetGun;

    [SerializeField]
    SphereCollider thisCollider;

    private void Start()
    {
        targetGun = transform.parent.parent.GetComponentInChildren<TargetGun>();
    }
}
