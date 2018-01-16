using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PoolObject
{
    public GameObject gameObject;
    public Bullet bullet;

    public PoolObject(GameObject gameObject, Bullet bullet)
    {
        this.gameObject = gameObject;
        this.bullet = bullet;
    }
}
