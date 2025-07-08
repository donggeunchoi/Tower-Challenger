using UnityEngine;

public class HardGamePlayer : MonoBehaviour
{
    private Vector3 Spawn;
    private StageManager stageManager;

    private void Start()
    {
        stageManager = StageManager.instance;
        Spawn = this.gameObject.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            stageManager.MiniGameResult(false);
            transform.position = Spawn;
        }
    }

    public void SetSpawn(Vector3 newSpawn)
    {
       
        Spawn = newSpawn;
    }
}
