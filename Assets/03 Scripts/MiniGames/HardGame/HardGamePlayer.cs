using UnityEngine;

public class HardGamePlayer : MonoBehaviour
{
    private Vector3 Spawn;

    private void Start()
    {
        Spawn = this.gameObject.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            if (StageManager.instance != null)
                StageManager.instance.MiniGameResult(false);

            transform.position = Spawn;
        }
    }

    public void SetSpawn(Vector3 newSpawn)
    {
       
        Spawn = newSpawn;
    }
}
