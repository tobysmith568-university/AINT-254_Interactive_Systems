using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField]
    PlayerGun player;

    void CanFire()
    {
        player.SendMessage("CanFire");
    }

    void CantFire()
    {
        player.SendMessage("CantFire");
    }
}
