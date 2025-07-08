using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TowerEnerance : MonoBehaviour
{
    public GameObject InventortyPanel;
    public GameObject MailPanel;
    public GameObject PausePanel;
    public GameObject EnterPanel;

    public void OnClickVillageMove()
    {
        SceneManager.LoadScene("VillageScene");
    }

    public void OnClickOnGameMove()
    {
        SceneManager.LoadScene("GameScene");
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
        Items.Instance.use = true;

        ItemData ticketItem = ItemManager.instance.items.Find(x => x.itemName == "타워 입장권 1매");
        
        if (ticketItem != null)
        {
            ItemManager.instance.items.Remove(ticketItem);
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            Debug.Log("타워 입장권이 없습니다.");
        }
    }

    public void OnClickEnter()
    {
        if (CheckItem("타워 입장권 1매"))
        {
            EnterPanel.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("GameScene");
            if (GameManager.Instance != null)
            {
                if (GameManager.Instance.mainStamina > 0)
                {
                    GameManager.Instance.UseStamina();
                }
                Debug.Log(GameManager.Instance.mainStamina);
            }
        }
    }

    public bool CheckItem(string name)
    {
        foreach (ItemData item in ItemManager.instance.items)
        {
            if (item.itemName == name)
            {
                return true;
            }

        }
        return false;
    }
    
}
