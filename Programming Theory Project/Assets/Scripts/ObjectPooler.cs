using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler PO;
    public List<GameObject> pooledUfo;
    public GameObject ufoToPool;
    public int amountToPool;

    void Awake()
    {
        PO = this;
    }

    // Start is called before the first frame update
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
        // For as many objects as are in the pooledObjects list
        for (int i = 0; i < pooledUfo.Count; i++)
        {
            // if the pooled objects is NOT active, return that object 
            if (!pooledUfo[i].activeInHierarchy)
            {
                return pooledUfo[i];
            }
        }
        // otherwise, return null   
        return null;
    }
    /*private void ResetUfoParameters(int j)
    {
        pooledUfo[j].GetComponent<MoveDownUFO>().healthUfo = GameManager.GM.healthUfo;
        pooledUfo[j].GetComponent<MoveDownUFO>().healthBar.maxValue = GameManager.GM.healthUfo;
        pooledUfo[j].GetComponent<MoveDownUFO>().isAimed = false;
        pooledUfo[j].GetComponent<MoveDownUFO>().counted = false;
        pooledUfo[j].GetComponent<MoveDownUFO>().ufoDown = false;
    }*/
}
