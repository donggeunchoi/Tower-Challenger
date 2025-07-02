using UnityEngine;

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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
