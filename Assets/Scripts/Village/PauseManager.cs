using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject SattingPanel;
    public GameObject PausePanel;

    public void OnClickSettingPanel()
    {
        SattingPanel.SetActive(true);
    }

    public void OnClickSettingClose()
    {
        SattingPanel.SetActive(false);
    }

    public void PauseClosePanel()
    {
        PausePanel.SetActive(false);
    }

    public void OnClickVillageMove()
    {
        SceneManager.LoadScene("VillageScene");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
