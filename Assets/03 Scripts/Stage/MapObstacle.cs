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
            difficulty = StageManager.instance.difficulty;
        }
        else
            difficulty = 1;

        return difficulty;
    }
}
