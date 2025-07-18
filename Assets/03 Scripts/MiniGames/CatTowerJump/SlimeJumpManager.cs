using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlimeJumpManager : MonoBehaviour
{
    public static SlimeJumpManager Instance;
    
    public BackGroundSpawner BackGroundspawner;
    public WallSpawner wallSpawner;
    public ObstacleSpawner obstacleSpawner;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ClearGame()
    {
        //이쪽에 다시 씬으로 넘기기
    }

    public void GameOver()
    {
        //여기에 패널 열어뿌지뭐
    }
}
