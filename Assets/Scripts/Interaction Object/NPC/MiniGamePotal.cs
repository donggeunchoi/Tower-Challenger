using UnityEngine;

public class MiniGamePotal : MonoBehaviour
{
    StageManager stageManager;

    private void Awake()
    {
        stageManager = StageManager.instance;
    }
    void Start()
    {
        stageManager.NextStage();
    }
}
