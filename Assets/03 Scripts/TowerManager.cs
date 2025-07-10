using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager towerInstance;

    public StageManager stageManager;

    private void Awake()
    {
        if (towerInstance == null)
        {
            towerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
