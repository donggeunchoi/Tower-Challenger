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
    
    [Header("인벤토리")]
    public GameObject Invetory;
    public GameObject InventoryItem;

    [Header("우편함")] 
    public GameObject MailBox;

    [Header("옷가게")] 
    public GameObject ClothesShopPanel;
    public GameObject ClothesShopItem;

    [Header("길드")] 
    public GameObject Guild;
    public GameObject GuildItem;

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
        }
    }
    

    #region 옷가게

    public void OnClickClothesShopClose()
    {
        ClothesShopPanel.SetActive(false);
    }

    public void OnClickClothesItemOpen()
    {
        ClothesShopItem.SetActive(true);
    }

    public void OnClickClothesShopItemClose()
    {
        ClothesShopItem.SetActive(false);
    }

    #endregion

    #region 길드

    public void OnClickGuildClose()
    {
        Guild.SetActive(false);
    }

    public void OnClickGuildItemOpen()
    {
        GuildItem.SetActive(true);
    }

    public void OnClickGuildItemClose()
    {
        GuildItem.SetActive(false);
    }

    #endregion
}
