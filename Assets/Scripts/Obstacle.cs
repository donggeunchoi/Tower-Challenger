using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Obstacle")] 
    public GameObject[] obstaclePrefabs;
    public Transform spawnPoint;
    public float spawnInterval = 2.5f;
    public float obstacleYoffset = 1f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
        Debug.Log(obstaclePrefabs);
    }

    // IEnumerator SpawnRoutine()
    // {
    //     while (!DinoMiniGame.Instance.isGameOver)
    //     {
    //         yield return new WaitForSeconds(spawnInterval);
    //         
    //         GroundTile[] allTiles = FindObjectsOfType<GroundTile>();
    //         var validGrounds = new System.Collections.Generic.List<GroundTile>();
    //
    //         foreach (var tile in allTiles)
    //         {
    //             if (!tile.isHole)
    //             {
    //                 validGrounds.Add(tile);
    //             }
    //         }
    //
    //         if (validGrounds.Count == 0)
    //         {
    //             yield break;
    //         }
    //         
    //         //랜덤한 타일 중 하나 고르기
    //         int index = Random.Range(0, validGrounds.Count);
    //         GroundTile selectedTile = validGrounds[index];
    //         
    //         //장애물 생성
    //         int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
    //         Vector3 spawnPos = selectedTile.transform.position + Vector3.up * obstacleYoffset;
    //         
    //         Instantiate(obstaclePrefabs[obstacleIndex], spawnPos, Quaternion.identity);
    //     }
    // }

    IEnumerator SpawnRoutine()
    {
        while (!DinoMiniGame.Instance.isGameOver)
        {
            yield return new WaitForSeconds(spawnInterval);

            // 스폰 포인트 아래 방향으로 레이 발사 (GroundTile을 찾기 위해)
            RaycastHit2D hit = Physics2D.Raycast(spawnPoint.position + Vector3.down * 0.1f, Vector2.down, 2f);

            if (hit.collider != null)
            {
                GroundTile tile = hit.collider.GetComponent<GroundTile>();

                // 아래에 GroundTile이 있고, 그것이 구덩이가 아니면 생성
                if (tile != null && !tile.isHole)
                {
                    int index = Random.Range(0, obstaclePrefabs.Length);
                    Instantiate(obstaclePrefabs[index], spawnPoint.position, Quaternion.identity);
                }
                else
                {
                    Debug.Log("❌ 구덩이 위라서 장애물 생성 안 함");
                }
            }
            else
            {
                Debug.Log("❌ 땅이 없어서 장애물 생성 안 함");
            }
        }
    }
    

}
