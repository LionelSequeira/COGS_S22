using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject pooledObject;
    public int pooledAmount;
    public bool willGrow;

    private List<GameObject> pooledObjectsList;

    // Start is called before the first frame update
    void Start()
    {
        pooledObjectsList = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = Instantiate(pooledObject, this.transform);
            obj.SetActive(false);
            pooledObjectsList.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < pooledObjectsList.Count; i++)
        {
            if(!pooledObjectsList[i].activeInHierarchy)
            {
                return pooledObjectsList[i];
            }
        }

        if(willGrow)
        {
            GameObject obj = Instantiate(pooledObject, this.transform);
            pooledObjectsList.Add(obj);
            return obj;
        }

        return null;
    }
}
