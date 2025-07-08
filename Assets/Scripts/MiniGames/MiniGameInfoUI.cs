using UnityEngine;

public class MiniGameInfoUI : MonoBehaviour
{
    private UIManager uiManager;
    public StageTimer stageTimer; 
    void Start()
    {
        uiManager = UIManager.Instance;
        
        stageTimer = uiManager.timerUI;
    }

    public void OnClickGameStart()
    {
        stageTimer.StartTimer();
    }
        
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        
    }
}
