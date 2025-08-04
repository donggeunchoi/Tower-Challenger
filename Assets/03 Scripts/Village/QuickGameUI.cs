using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickGameUI : MonoBehaviour
{
    public GameObject panel;
    public TMPro.TMP_Text warningText;
    public TMPro.TMP_Text warningText2;
    public GameObject Panel2;
    
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

            if (GameManager.Instance.stamina.mainStamina <= 0)
            {
                if (warningText == null) return;
                if (warningText2 == null) return;
                
                
                warningText.text = "스테미나가 없습니다.";
                warningText2.text = "스테미나가 없습니다.";
                StartCoroutine(WarningText());
                
                
                return;
            }
            
            
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
        panel.SetActive(false);
    }


    IEnumerator WarningText()
    {
        yield return new WaitForSeconds(1f);

        if (warningText != null)
        {
            warningText.text = "스테미나를 사용해서 타워에 입장하겠습니까?";
        }

        if (warningText2 != null)
        {
            warningText2.text = "타워 입장권을 사용해서 타워로 들어가겠습니까?\nNo - 스테미나 사용해서 입장합니다";
        }
        
    }

    public void OnCllickCancel2()
    {
        Panel2.SetActive(false);
    }
}
