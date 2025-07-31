using System;
using System.Collections;
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

    [Header("우편함")] 
    public GameObject MailBox;

    [Header("옷가게")] 
    public GameObject ClothesShopPanel;
    public GameObject ClothesShopItem;

    [Header("길드")] 
    public GameObject Guild;

    [Header("설정")] 
    public GameObject StopPanel;
    
    [Header("캐릭터 선택")]
    public GameObject CharactorChoicePanel;

    [Header("판넬 관리")]
    public Transform popupGroup;
    public GameObject popup;

    [Header("튜토리얼")] 
    public Canvas tutorialCanvas;
    private bool isTutorial;


    private void Awake()
    {
        // if (!GameManager.Instance.playerData.VillageTutorialCompleted)
        // {
        //     GameManager.Instance.playerData.VillageTutorialCompleted = true;
        //     tutorialCanvas.gameObject.SetActive(true);
        // }

        if (!isTutorial)
        {
            isTutorial = true;
            tutorialCanvas.gameObject.SetActive(true);
        }
    }

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
                if (popup != null)
                    Destroy(popup);
                popup = Instantiate(StorePanel, popupGroup);
                break;
            case "Invetory":
                if (popup != null)
                    Destroy(popup);
                popup = Instantiate(Invetory, popupGroup);
                break;
            case "MailBox":
                MailBox.SetActive(true);
                break;
            case "ClothesShopPanel":
                if (popup != null)
                    Destroy(popup);
                popup = Instantiate(ClothesShopPanel, popupGroup);
                break;
            case "Guild":
                if (popup != null)
                    Destroy(popup);
                popup = Instantiate(Guild, popupGroup);
                break;
            case "PausePanel":
                StopPanel.SetActive(true);
                Time.timeScale = 0;
                break;
            case "CharactorChoicePanel":
                if (popup != null)
                    Destroy(popup);
                popup = Instantiate(CharactorChoicePanel, popupGroup);
                break;
            case "TowerEntrance":
                SceneManager.LoadScene("TowerEntrance");
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

    public void OnClickMailBox()
    {
        MailBox.SetActive(true);
    }

    public void OnClickMailBoxClose()
    {
        MailBox.SetActive(false);
    }
    
    public void OnClickEndTutorial()
    {
       tutorialCanvas.gameObject.SetActive(false);
    }
    
}
