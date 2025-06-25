using UnityEngine;
using System.Collections;

public class MiniGamePotal : MonoBehaviour
{
    StageManager stageManager;

    private void Awake()
    {
        stageManager = StageManager.instance;
    }

    private void Start()
    {
        stageManager.StartNextMiniGame();
    }
}
