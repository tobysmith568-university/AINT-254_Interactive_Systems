using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public struct PooledObject
{
    public string name;
    public GameObject prefab;
    public int amount;
}

public class PoolManager : MonoBehaviour
{
    [SerializeField]
    PooledObject[] pooledObjects;

    private List<PoolObject> bulletPool = new List<PoolObject>();

    //void OnEnable()
    //{
    //    current = this;
    //}

    void Start()
    {
        //Creates all of the objects

        for (int i = 0; i < pooledObjects.Length; i++)
        {
            List<GameObject> objList = new List<GameObject>();

            for (int j = 0; j < pooledObjects[i].amount; j++)
            {
                GameObject obj = Instantiate(pooledObjects[i].prefab, transform);
                obj.SetActive(false);
                objList.Add(obj);

                bulletPool.Add(new PoolObject(obj, obj.GetComponent<Bullet>()));
            }
        }
    }

    /// <summary>
    /// Finds a pooled object which is currently not active - or returns null
    /// </summary>
    /// <returns>The PoolObject</returns>
    public PoolObject GetPooledObject()
    {
        for (int i = 0; i < bulletPool.Count; i++)
            if (!bulletPool[i].gameObject.activeInHierarchy)
                return bulletPool[i];
        return null;
    }
}
