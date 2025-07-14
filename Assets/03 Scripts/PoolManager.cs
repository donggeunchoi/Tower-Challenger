using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    private Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject GetObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        string key = prefab.name;

        if (poolDictionary.ContainsKey(key) && poolDictionary[key].Count > 0)
        {
            GameObject obj = poolDictionary[key].Dequeue();

            if (obj == null)
                return Instantiate(prefab, position, rotation);

            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);

            return obj;
        }
        else
        {
            GameObject newObj = Instantiate(prefab, position, rotation);
            newObj.name = prefab.name;
            return newObj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        string key = obj.name;

        if (!poolDictionary.ContainsKey(key))
        {
            poolDictionary[key] = new Queue<GameObject>();
        }

        obj.transform.SetParent(this.transform);
        obj.SetActive(false);
        poolDictionary[key].Enqueue(obj);
    }
}