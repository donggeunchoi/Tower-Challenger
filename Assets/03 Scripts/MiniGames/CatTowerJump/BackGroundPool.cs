using System.Collections.Generic;
using UnityEngine;

public class BackGroundPool : MonoBehaviour
{
    public static BackGroundPool Instance;
    
    public GameObject BackGroundPrefab;
    public Transform BackGroundContainer;
    public int BackGroundSize;
    
    
    private Queue<GameObject> backGroundPool;
    
    private void Awake()
    {
        Instance = this;
        backGroundPool = new Queue<GameObject>();
        InitializePoolBG();
    }

    private void InitializePoolBG()
    {
        for (int i = 0; i < BackGroundSize; i++)
        {
            GameObject BackTile = Instantiate(BackGroundPrefab, BackGroundContainer);
            BackTile.SetActive(false);
            backGroundPool.Enqueue(BackTile);
            
        }
    }

    public GameObject GetBackGround()
    {
        if (backGroundPool.Count > 0)
        {
            GameObject BackTile = backGroundPool.Dequeue();
            BackTile.SetActive(true);
            return BackTile;
        }
        else
        {
            GameObject BackTile = Instantiate(BackGroundPrefab, BackGroundContainer);
            return BackTile;
        }
    }

    public void ReturnBackGround(GameObject BackTile)
    {
        BackTile.SetActive(false);
        backGroundPool.Enqueue(BackTile);
    }
}
