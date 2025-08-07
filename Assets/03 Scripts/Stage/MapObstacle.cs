using UnityEngine;

public class MapObstacle : MonoBehaviour
{
    [SerializeField] private DiffcultyObstacles[] obstacles;

    [SerializeField] private int difficulty;

    public void Init()
    {
        this.gameObject.SetActive(true);

        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].gameObject.SetActive(false);
        }

        int diffcultyIndex = CheckDiffculty();

        if (diffcultyIndex >= 0 && diffcultyIndex < obstacles.Length && obstacles[diffcultyIndex] != null)
        {
            obstacles[diffcultyIndex].gameObject.SetActive(true);
            obstacles[diffcultyIndex].Init();
        }
    }

    public int CheckDiffculty()
    {
        if (StageManager.instance != null)
        {
            return Mathf.Clamp(StageManager.instance.difficulty - 1, 0, obstacles.Length - 1);
        }
        else
            return 0;
    }
}
