using System.Collections.Generic;
using UnityEngine;

public class ObstaclePoolManager : MonoBehaviour
{
    public static ObstaclePoolManager Instance;

    [Header("장애물 종류")] 
    public GameObject spinObstacle;
    public GameObject moveObstacle;
    public GameObject wallObstacle;

    [Header("장애물 세팅")] 
    public Transform ObstacleContainer;
    public int initialSize;

    private Queue<GameObject> spinPool;
    private Queue<GameObject> movePool;
    private Queue<GameObject> wallPool;

    private void Awake()
    {
        Instance = this;
        
        spinPool = new Queue<GameObject>();
        movePool = new Queue<GameObject>();
        wallPool = new Queue<GameObject>();

        for (int i = 0; i < initialSize; i++)
        {
            GameObject obstacle = Instantiate(spinObstacle, ObstacleContainer);
            obstacle.SetActive(false);
            spinPool.Enqueue(obstacle);
        }

        for (int i = 0; i < initialSize; i++)
        {
            GameObject obstacle = Instantiate(moveObstacle, ObstacleContainer);
            obstacle.SetActive(false);
            movePool.Enqueue(obstacle);
        }

        for (int i = 0; i < initialSize; i++)
        {
            GameObject obstacle = Instantiate(wallObstacle, ObstacleContainer);
            obstacle.SetActive(false);
            wallPool.Enqueue(obstacle);
        }
    }

    public GameObject GetSine()
    {
        if (spinPool.Count > 0)
        {
            GameObject spinObject = spinPool.Dequeue();
            spinObject.SetActive(true);
            return spinObject;
        }
        GameObject obstacle = Instantiate(spinObstacle, ObstacleContainer);
        return obstacle;
    }

    public GameObject GetMove()
    {
        if (movePool.Count > 0)
        {
            GameObject moveObject = movePool.Dequeue();
            moveObject.SetActive(true);
            return moveObject;
        }
        return Instantiate(moveObstacle, ObstacleContainer);
    }

    public GameObject GetWall()
    {
        if (wallPool.Count > 0)
        {
            GameObject wallObject = wallPool.Dequeue();
            wallObject.SetActive(true);
            return wallObject;
        }
        return Instantiate(wallObstacle, ObstacleContainer);
    }

    public void ReturnObstacle(GameObject obstacle)
    {
        obstacle.SetActive(false);

        if (obstacle.GetComponent<SpinnerObstacle>() != null)
        {
            spinPool.Enqueue(obstacle);
        }
        else if(obstacle.GetComponent<HorizontalMoverObstacle>() != null)
        {
            movePool.Enqueue(obstacle);
        }
        else if(obstacle.GetComponent<DamageTile>() != null)
        {
            wallPool.Enqueue(obstacle);
        }
        else
        {
            movePool.Enqueue(obstacle);
        }
    }
}
