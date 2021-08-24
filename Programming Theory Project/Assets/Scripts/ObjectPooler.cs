using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler PO { get; private set; }                 //ENCAPSULATION
    public List<GameObject> pooledUfo;
    public GameObject ufoToPool;
    public int amountToPool;

    void Awake()
    {
        PO = this;
    }
    void Start()
    {
        // Loop through list of pooled objects,deactivating them and adding them to the list 
        pooledUfo = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(ufoToPool);
            obj.SetActive(false);
            pooledUfo.Add(obj);
            obj.transform.SetParent(this.transform); // set as children of Spawn Manager
        }
    }

    public GameObject GetPooledUfo()
    {
        for (int i = 0; i < pooledUfo.Count; i++)
        {
            if (!pooledUfo[i].activeInHierarchy)
            {
                return pooledUfo[i];
            }
        }        
        return null;
    }    
}
