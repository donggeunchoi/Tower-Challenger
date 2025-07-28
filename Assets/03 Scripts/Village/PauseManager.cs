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
        Time.timeScale = 1;
    }

    public void OnClickVillageMove()
    {
        SceneManager.LoadScene("VillageScene");
    }
    
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_ANDROID || UNITY_IOS
            Application.Quit();
#endif
    }
    
}
