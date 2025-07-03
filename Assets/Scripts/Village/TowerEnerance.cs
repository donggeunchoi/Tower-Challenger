using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TowerEnerance : MonoBehaviour
{
    public GameObject InventortyPanel;
    public GameObject MailPanel;
    public GameObject PausePanel;

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
    
}
