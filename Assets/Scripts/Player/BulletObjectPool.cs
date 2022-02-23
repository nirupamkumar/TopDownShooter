using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : MonoBehaviour
{
    public static BulletObjectPool bulletObjectPoolInstance;

    public GameObject bulletPrefab;

    [SerializeField] private int poolDepthAmount;

    private readonly List<GameObject> bulletPool = new List<GameObject>();

    private void Awake()
    {
        bulletObjectPoolInstance = this;

        for (int i = 0; i < poolDepthAmount; i++)
        {
            GameObject poolObject = Instantiate(bulletPrefab);
            bulletPool.Add(poolObject);
            poolObject.transform.parent = this.transform;
        }
    }

    public GameObject GetPoolObject()
    {
        for(int i=0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].activeInHierarchy)
            {
                return bulletPool[i];
            }
        }

        return null;
    }

    

}
