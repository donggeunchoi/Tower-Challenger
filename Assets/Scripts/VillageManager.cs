using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VillageManager : MonoBehaviour
{
    public GameObject StorePanel;
    public GameObject Item1;
    public GameObject Item2;
    public GameObject Item3;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickStore()
    {
        StorePanel.SetActive(true);
    }

    public void OnClickOutStore()
    {
        StorePanel.SetActive(false);
    }

    public void OnClickTowerMove()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnClickItem1()
    {
        Item1.SetActive(true);
    }

    public void OnClickItem2()
    {
        Item2.SetActive(true);
    }

    public void OnclickItem3()
    {
        Item3.SetActive(true);
    }

    public void OnClickItemRemove()
    {
        if(Item1 != null)
        {
            Item1.SetActive(false);
            return;
        }

        if(Item2 != null)
        {
            Item2.SetActive(false);
            return;
        }
        
        if(Item3 != null)
        {
            Item3.SetActive(false);
        }
    }
}
