using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    private StageManager stageManager;

    private void Awake()
    {
        stageManager = StageManager.Instance;
    }

    private void Start()
    {
        Debug.Log(stageManager);
        stageManager.StartGame();
    }
}
