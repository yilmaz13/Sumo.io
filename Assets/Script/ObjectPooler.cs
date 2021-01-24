using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object Pooler
/// - allows the reuse of frequently "spawned" objects for optimization
/// </summary>
public sealed class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string poolName;
        public List<GameObject> pooledObjects;
        public GameObject prefab;
        public Transform startingParent;
        public int startingQuantity = 10;
    }

    public Pool[] pools;

    // ObjectPooler is singleton so we access this in global 
    public static ObjectPooler Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    /// <summary>
    /// Pool objects spawn in game arena. But that is setactive false
    /// </summary>
    void Start()
    {
        for (int p = 0; p < pools.Length; p++)
        {
            GameObject poolParent = new GameObject();
            poolParent.name = pools[p].poolName;
            poolParent.transform.parent = transform;
            for (int i = 0; i < pools[p].startingQuantity; i++)
            {
                GameObject o = Instantiate(pools[p].prefab, Vector3.zero, Quaternion.identity, poolParent.transform);
                o.GetComponent<PoolObject>().parent = poolParent.transform;
                pools[p].pooledObjects.Add(o);
                o.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Object spawn in game arena
    /// </summary>
    /// <param name="poolName"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="parentTransform"></param>
    /// <returns></returns>
    public GameObject Spawn(string poolName, Vector3 position, Quaternion rotation, Transform parentTransform = null)
    {
        // Find the pool that matches the pool name:
        int pool = 0;
        for (int i = 0; i < pools.Length; i++)
        {
            if (pools[i].poolName == poolName)
            {
                pool = i;
                break;
            }

            if (i == pools.Length - 1)
            {
                Debug.LogError("There's no pool named \"" + poolName +
                               "\"! Check the spelling or add a new pool with that name.");
                return null;
            }
        }

        for (int i = 0; i < pools[pool].pooledObjects.Count; i++)
        {
            if (!pools[pool].pooledObjects[i].activeSelf)
            {
                // Set active:
                pools[pool].pooledObjects[i].SetActive(true);
                pools[pool].pooledObjects[i].transform.localPosition = position;
                pools[pool].pooledObjects[i].transform.localRotation = rotation;
                // Set parent:
                if (parentTransform)
                {
                    pools[pool].pooledObjects[i].transform.SetParent(parentTransform, false);
                }

                return pools[pool].pooledObjects[i];
            }
        }

        // If there's no game object available then expand the list by creating a new one:
        GameObject o = Instantiate(pools[pool].prefab, position, rotation);

        // Add newly instantiated object to pool:
        pools[pool].pooledObjects.Add(o);
        return o;
    }
}