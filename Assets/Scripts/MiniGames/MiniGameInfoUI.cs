using UnityEngine;

public class MiniGameInfoUI : MonoBehaviour
{
    private UIManager uiManager;
    public StageTimer stageTimer; 
    void Start()
    {
        if (UIManager.Instance != null)
        {
            uiManager = UIManager.Instance;
        }

        if (uiManager != null)
        {
            stageTimer = uiManager.timerUI;
        }
        
        if (stageTimer != null)
        {
            stageTimer.StopTimer();
            Time.timeScale = 0;
        }
        
    }

    public void OnClickGameStart()
    {
        if (uiManager != null)
        {
            stageTimer.StartTimer();
            Time.timeScale = 1;
            Destroy(this.gameObject);
            
        }
    }
        
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        
    }
}
