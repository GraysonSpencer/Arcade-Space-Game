using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Taken From https://www.raywenderlich.com/847-object-pooling-in-unity
// Modified slightly so that null is not returned from GetPooledObject.


/// <summary>
/// Represents an item that can be contained in the object pooler.
/// </summary>
[System.Serializable]
public class ObjectPoolItem
{
    /// <summary>
    /// If the number of objects can exceed the intial capacity given by amountToPool.
    /// </summary>
    public bool shouldExpand;
    
    /// <summary>
    /// Type of object to pool.
    /// </summary>
    public GameObject objectToPool;
    
    /// <summary>
    /// Number of objects that should be pre-allocated.
    /// </summary>
    public int amountToPool;
}

/// <summary>
/// Manages memory for all game objects.
/// </summary>
public class ObjectPooler : MonoBehaviour
{
    /// <summary>
    /// Instance of the object pooler to access all stored game objects.
    /// </summary>
    public static ObjectPooler sharedInstance;
    
    /// <summary>
    /// Contains all pooled objects.
    /// </summary>
    public List<GameObject> pooledObjects;

    /// <summary>
    /// List of all objects to store in the pool.
    /// </summary>
    public List<ObjectPoolItem> itemsToPool;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        pooledObjects = new List<GameObject>();

        // Pre-allocate all game objects that request to be allocated.
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }


    void Awake()
    {
        sharedInstance = this;
    }

    /// <summary>
    /// Retrieve pooled object of the given tag.
    /// If all objects are currently active a new object may be generated if the object is allowed to expand.
    /// </summary>
    /// <param name="tag">Tag associated with object to be retrieved.</param>
    /// <param name="objectToBeRetrieved">The object to be returned or an empty game object.</param>
    /// <returns>True if a valid object was returned, false otherwise.</returns>
    public bool GetPooledObject(string tag, out GameObject objectToBeRetrieved)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                objectToBeRetrieved = pooledObjects[i];
                return true;
            }
        }
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.tag == tag)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    objectToBeRetrieved = obj;
                    return true;
                }
            }
        }

        objectToBeRetrieved = new GameObject();
        return false;
    }

    /// <summary>
    /// Disables anything that leaves play area.
    /// Play area is a box collider 2D. 
    /// </summary>
    /// <param name="other">Object that exited the play area.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        other.gameObject.SetActive(false);
    }
}