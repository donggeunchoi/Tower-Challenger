using UnityEngine;



public class MapObstacle : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacleMap;

    [SerializeField] private int difficulty;


    private void Start()
    {
        for (int i = 0; i < obstacleMap.Length; i++)
        {
            obstacleMap[i].gameObject.SetActive(false);
        }
    }

    private void Init()
    {
        difficulty = CheckDifficuty();
        int mapDif = Mathf.Min(difficulty, obstacleMap.Length);

        for (int i = 0; i < mapDif; i++)
        {
            obstacleMap[i].gameObject.SetActive(true);
        }
    }

    private int CheckDifficuty()
    {
        //if (StageManager.instance != null)
        //{
        //    StageManager.instance.floor 


        //    switch (StageManager.instance.floor)
        //    {
        //        case 
        //    }
        //}

        return difficulty;
    }


}
