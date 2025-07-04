using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Obstacle")] 
    public GameObject[] obstaclePrefabs;
    public Transform spawnPoint;
    public float spawnInterval = 2.5f;
    public Transform obstaclesContainer;
    public Vector3[] obstacleOffsets;
    

    void Start()
    {
        if (StageManager.instance != null && GameManager.Instance != null)
        {
            int deficult = StageManager.instance.deficult;

            MiniGameData data = GameManager.Instance.miniGameDataList.Find(x => x.name == "SlimeRun" && x.DifficultyLevel == deficult);

            if (data != null)
            {
                spawnInterval = data.spawnInterval;
            }
        }

        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (!DinoMiniGame.instance.isGameOver)
        {
            yield return new WaitForSeconds(spawnInterval);

            // 스폰 포인트 아래 방향으로 레이 발사 (GroundTile을 찾기 위해)
            RaycastHit2D hit = Physics2D.Raycast(spawnPoint.position + Vector3.down * 0.1f, Vector2.down, 2f);
            if (hit.collider == null)
            { 
                Debug.Log("구덩이 위라서 장애물 생성 안 함");
                continue;
            }
            
            GroundTile tile = hit.collider.GetComponent<GroundTile>();
            if (tile == null || tile.isHole)
            {
                Debug.Log("땅이 없어서 장애물 생성 안 함");
                continue;
            }
            
            Vector2 prevCheckPos = hit.collider.transform.position + Vector3.left * tile.tileWidth;
            RaycastHit2D prevHit = Physics2D.Raycast(prevCheckPos + Vector2.down * 0.1f, Vector2.down, 2f);
            
            bool isAfterHole = false;
            if (prevHit.collider != null)
            {
                GroundTile prevTile = prevHit.collider.GetComponent<GroundTile>();
                if (prevTile != null && prevTile.isHole)
                {
                    isAfterHole = true;
                }
            }

            if (isAfterHole)
            {
                Debug.Log("구덩이 바로 뒤이므로 장애물 생성 안 함");
                continue;
            }
            
            int index = Random.Range(0, obstaclePrefabs.Length);
            Vector3 offset = obstacleOffsets[index];
            Instantiate(obstaclePrefabs[index], spawnPoint.position + offset, Quaternion.identity, obstaclesContainer);
          
        }
    }
    

}
