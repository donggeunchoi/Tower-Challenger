using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    private StageManager stageManager;

    private void Awake()
    {
        stageManager = StageManager.instance;
    }

    private void Start()
    {
        stageManager.StartGame();
    }


}
