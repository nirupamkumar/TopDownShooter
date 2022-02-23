using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingSystem
{
    Stack<GameObject> pool = new Stack<GameObject>();
    GameObject gameObject;

    public PoolingSystem(GameObject gameObject) 
    {
        this.gameObject = gameObject;
    }

    public GameObject GetObject() 
    {
        return GetObject(Vector3.zero, Quaternion.identity);
    }

    public GameObject GetObject(Vector3 position, Quaternion rotation) 
    {
        if (pool.Count > 0)
        {
            var destackedObject = pool.Pop();
            destackedObject.SetActive(true);
            destackedObject.transform.position = position;
            destackedObject.transform.rotation = rotation;
            return destackedObject;
        }

        var instantiatedObject = GameObject.Instantiate(gameObject, position, rotation);

        if (instantiatedObject.GetComponent<Poolable>() == null)
        {
            var poolable = instantiatedObject.AddComponent<Poolable>();
            poolable.parentPool = this;
        }
        return instantiatedObject;
    }

    public void ReturnPoolableToPool(Poolable objectToReturn) 
    {
        objectToReturn.gameObject.SetActive(false);
        pool.Push(objectToReturn.gameObject);
    }
}

public class Poolable : MonoBehaviour 
{
    public PoolingSystem parentPool;
    public void ReturnToPool() 
    {
        parentPool.ReturnPoolableToPool(this);
    }
}
