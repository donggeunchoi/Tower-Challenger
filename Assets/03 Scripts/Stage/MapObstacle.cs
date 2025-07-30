using UnityEngine;

public class MapObstacle : MonoBehaviour
{
    [SerializeField] private GameObject[] mapObstacle;

    [SerializeField] private int difficulty;

    public void Init()
    {
        for (int i = 0; i < mapObstacle.Length; i++)
        {
            mapObstacle[i].gameObject.SetActive(false);
        }

        difficulty = CheckDifficuty();
        int mapDif = Mathf.Min(difficulty, mapObstacle.Length);

        for (int i = 0; i < mapDif; i++)
        {
            mapObstacle[i].gameObject.SetActive(true);
        }
    }

    private int CheckDifficuty()
    {
        if (StageManager.instance != null)
        {
            if (StageManager.instance.floor <= 5)
                difficulty = 1;
            else if (StageManager.instance.floor <= 10)
                difficulty = 2;
            else if (StageManager.instance.floor <= 15)
                difficulty = 3;
            else if (StageManager.instance.floor <= 20)
                difficulty = 4;
            else if (StageManager.instance.floor <= 25)
                difficulty = 5;
            else
                difficulty = 6;
        }
        else
            difficulty = 1;

        return difficulty;
    }
}
