using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VillageManager : MonoBehaviour
{
    [Header("fade")]
    public Image fadePanel;
    public float fadeDuration;
    
    [Header("아이템 상점")]
    public GameObject StorePanel;
    public GameObject Item1;
    public GameObject Item2;
    public GameObject Item3;
    
    [Header("인벤토리")]
    public GameObject Invetory;

    [Header("우편함")] 
    public GameObject MailBox;

    [Header("옷가게")] 
    public GameObject ClothesShopPanel;

    [Header("길드")] 
    public GameObject Guild;

    [Header("설정")] 
    public GameObject StopPanel;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fadePanel.gameObject.SetActive(true);
        fadePanel.color = new Color(0, 0, 0, 1);
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float time = 0;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, time/fadeDuration);
            fadePanel.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadePanel.gameObject.SetActive(false);
    }

    public void VillageMove(string villageName)
    {
        switch (villageName)
        {
            case "StorePanel":
                StorePanel.SetActive(true);
                break;
            case "Invetory":
                Invetory.SetActive(true);
                break;
            case "MailBox":
                MailBox.SetActive(true);
                break;
            case "ClothesShopPanel":
                ClothesShopPanel.SetActive(true);
                break;
            case "Guild":
                Guild.SetActive(true);
                break;
            case "StopPanel":
                StopPanel.SetActive(true);
                break;
            case "GameScene":
                SceneManager.LoadScene("GameScene");
                break;
            
            default:
                break;
            
            
        }
    }

    public void OnClickOutStore()
    {
        StorePanel.SetActive(false);
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

    public void OnClickItemRemove(int itemnumber)
    {
        switch (itemnumber)
        {
            case 0:
                Item1.SetActive(false);
                break;
            case 1:
                Item2.SetActive(false);
                break;
            case 2:
                Item3.SetActive(false);
                break;
        }
    }
}
