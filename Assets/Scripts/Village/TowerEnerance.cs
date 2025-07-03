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
    }

    public void OnClickMail()
    {
        MailPanel.SetActive(true);
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
        Inventory.instance.RemoveItem("타워 입장권");
        SceneManager.LoadScene("GameScene");
    }

    public void OnClickEnter()
    {
        if (CheckItem("타워 입장권"))
        {
            EnterPanel.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("GameScene");
            // //이곳에서 스테미너 사용해서 들어가기
            // GameManager.Instance.UseStamina();
            // Debug.Log(GameManager.Instance.mainStamina);
        }
    }

    public bool CheckItem(string name)
    {
        if (ItemManager.instance.itemNames.Contains(name))
        {
            EnterPanel.SetActive(true);
            return true;
        }
        else
        {
            Debug.Log("아이템 없음");
            return false;
        }
        
    }
    
}
