using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object pool
/// </summary>
public abstract class ObjectPool : MonoBehaviour
{
    /// <summary>
    /// Initializes the pool
    /// </summary>
    public abstract void Initialize();

    /// <summary>
    /// Gets an object from the pool
    /// </summary>
    /// <returns>object</returns>
    protected GameObject GetObject(List<GameObject> objectPool, GameObject objectPrefab)
    {
        // check for available object in pool
        if (objectPool.Count > 0)
        {
            GameObject obj = objectPool[objectPool.Count - 1];
            objectPool.RemoveAt(objectPool.Count - 1);
            return obj;
        }
        else
        {
            Debug.Log("Growing pool ...");

            // pool empty, so expand pool and return new object
            objectPool.Capacity++;
            return this.GetNewObject(objectPrefab);
        }
    }

    /// <summary>
    /// Returns an object to the pool
    /// </summary>
    /// <param name="obj">object</param>
    protected void ReturnObject(List<GameObject> objectPool ,GameObject obj)
    {
        objectPool.Add(obj);
        obj.SetActive(false);
    }

    /// <summary>
    /// gets new object for the given prefab
    /// </summary>
    /// <returns></returns>
    protected GameObject GetNewObject(GameObject objectPrefab)
    {
        GameObject obj = Instantiate(objectPrefab);
        obj.SetActive(false);
        DontDestroyOnLoad(obj);
        return obj;
    }
}
