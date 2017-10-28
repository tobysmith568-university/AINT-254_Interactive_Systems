using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField]
    PlayerGun player;

    /// <summary>
    /// Called by the hand animation to enable fire at the end of the recoil animation
    /// </summary>
    void CanFire()
    {
        player.SendMessage("CanFire");
    }

    /// <summary>
    /// Called by the hand animation to disable fire at the begining of the recoil animation
    /// </summary>
    void CantFire()
    {
        player.SendMessage("CantFire");
    }
}
