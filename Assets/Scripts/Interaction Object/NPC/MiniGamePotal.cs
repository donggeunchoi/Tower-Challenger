using UnityEngine;
using System.Collections;

public class MiniGamePotal : MonoBehaviour
{
    StageManager stageManager;

    private void Awake()
    {
        stageManager = StageManager.Instance;
    }

    private void Start()
    {
        stageManager.StartNextMiniGame();
    }
}
