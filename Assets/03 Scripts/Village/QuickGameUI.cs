using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickGameUI : MonoBehaviour
{
    public GameObject Panel;
    
    public void OnClickUseTicket()
    {
        //Items.Instance.use = true;

        ItemData ticketItem = ItemManager.instance.items.Find(x => x.type == ItemType.TopTicket);

        if (ticketItem != null)
        {
            ItemManager.instance.items.Remove(ticketItem);
            
            bool tutorial = GameManager.Instance.playerData.tutorialCompleted;
            if (!tutorial)
            {
                SceneManager.LoadScene("TutorialScene");
            }
            else
            {
                SceneManager.LoadScene("GameScene");
            }
        }
        else
        {
            Debug.Log("타워 입장권이 없습니다.");
        }
    }

    public void OnClickInGameStamina()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.stamina.mainStamina > 0)
            {
                GameManager.Instance.stamina.UseStamina();
            }
        }
        
        bool tutorial = GameManager.Instance.playerData.tutorialCompleted;
        if (!tutorial)
        {
            SceneManager.LoadScene("TutorialScene");
        }
        else
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    public void OnClickCancel()
    {
        Panel.SetActive(false);
    }
}
