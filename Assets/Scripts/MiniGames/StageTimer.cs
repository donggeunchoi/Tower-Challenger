using UnityEngine;

public class StageTimer : MonoBehaviour
{
    public float timer = 60f;

    StageManager stageManager;

    private void Start()
    {
        stageManager = StageManager.instance;
    }

    private void Update()
    {
        if (stageManager.isGameActive)
            timer -= Time.deltaTime;
    }
}
