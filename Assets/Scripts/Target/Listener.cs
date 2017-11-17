using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listener : MonoBehaviour
{
    public TargetGun targetGun;

    private void Start()
    {
        targetGun = transform.parent.parent.GetComponentInChildren<TargetGun>();
    }
}
