﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TowerEnerance : MonoBehaviour
{
    public GameObject InventortyPanel;
    public GameObject MailPanel;
    public GameObject PausePanel;
    public GameObject EnterPanel;
    public GameObject quickStaminaUI;
    public GameObject quickTicketUI;
    public void OnClickVillageMove()
    {
        SceneManager.LoadScene("VillageScene");
    }

    public void OnClickOnGameMove()
    {
        SceneManager.LoadScene("TutorialScene");
        //이곳에서도 스테미너 호출해야합니다.
    }

    public void OnClickMail()
    {
        MailPanel.SetActive(true);
    }

    public void OnClickMailClose()
    {
        MailPanel.SetActive(false);
    }

    public void OnClickInventorty()
    {
        InventortyPanel.SetActive(true);
    }

    public void OnClickPause()
    {
        PausePanel.SetActive(true);
    }

    public void OnClickUseTicket()
    {
        //Items.Instance.use = true;

        ItemData ticketItem = ItemManager.instance.items.Find(x => x.type == ItemType.TopTicket);

        if (ticketItem != null)
        {
            ItemManager.instance.items.Remove(ticketItem);
            SceneManager.LoadScene("TutorialScene");
        }
        else
        {
            Debug.Log("타워 입장권이 없습니다.");
        }
    }

    public void OnClickEnter()
    {
        if (CheckItem())
        {
            EnterPanel.SetActive(true);
        }
        else
        {

            quickStaminaUI.SetActive(true);


            //bool tutorial = GameManager.Instance.playerData.tutorialCompleted;
            //if (!tutorial)
            //{
            //    SceneManager.LoadScene("TutorialScene");
            //}
            //else
            //{
            //    if (GameManager.Instance.stamina.mainStamina > 0)
            //        SceneManager.LoadScene("GameScene");
            //}
           
        }
    }

    public bool CheckItem()
    {
        foreach (ItemData item in ItemManager.instance.items)
        {
            if (item.type == ItemType.TopTicket)
            {
                return true;
            }

        }
        return false;
    }

}
