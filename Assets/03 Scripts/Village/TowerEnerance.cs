using UnityEngine;
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
        if (GameManager.Instance.stamina.mainStamina > 0)
        {
            GameManager.Instance.stamina.UseStamina();
        }
        else
        {
            Debug.Log("스테미나가 없습니다");
        }
        
        
        bool tutorial = GameManager.Instance.playerData.tutorialCompleted;
        if (!tutorial)
        {
            SceneManager.LoadScene("TutorialScene");
        }
        else
        {
            if (GameManager.Instance.stamina.mainStamina > 0)
                SceneManager.LoadScene("GameScene");
        }
        
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
            bool tutorial = GameManager.Instance.playerData.tutorialCompleted;
            if (!tutorial)
            {
                ItemManager.instance.items.Remove(ticketItem);
                SceneManager.LoadScene("TutorialScene");
            }
            else
            {
                ItemManager.instance.items.Remove(ticketItem);
                SceneManager.LoadScene("GameScene");
            }
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
