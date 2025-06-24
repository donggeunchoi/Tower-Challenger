using UnityEngine;

public class NextFoorPotal : MonoBehaviour
{
    StageManager stageManager;

    private void Awake()
    {
        stageManager = StageManager.instance;
    }

    private void Start()
    {
        stageManager.NextStage();
    }
}
