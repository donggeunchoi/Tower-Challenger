using UnityEngine;

public class Dino_Ground_Spawner : MonoBehaviour
{
    public GameObject groundPrefabs;
    public GameObject groundHolePrefab;

    public int groundCount;
    public float groundWidth = 6;
    public float holeWidth = 6;
    public float groundX;

    private bool wasHole = false;
    private bool isHole = false;

    public Vector3 groundScale;

    public void GroundSpawner()
    {
        //var widths = TileWidth();
        //float groundW = widths.

        for (int i = 0; i < groundCount; i++)
        {
            if (!wasHole) // false 이전 땅이 구멍이 아니라면
            {
                isHole = Random.value < 0.3f; // 구멍 30%확률로 생성
            }
            else
            {
                isHole = false;
            }

            Vector2 pos = new Vector2(groundX + i * GraoundWidth(), 0);

            GameObject prefab = isHole ? groundHolePrefab : groundPrefabs;

            GameObject ground = Instantiate(prefab, pos, Quaternion.identity);

            ground.transform.localScale = groundScale;

            wasHole = isHole; // 구멍이 생성됬다면 true를 아니라면 false를
        }
    }
    public float GraoundWidth()
    {
        var obj = Dino_Ground_Manager.instance.move;
        if (obj.groundSprite != null)
        {
            return obj.groundSprite.bounds.size.x;
        }
        else
        {
            Debug.Log("groundSprite is null");
        }
        return groundWidth;
    }
    //public (float _groundWidth, float _holeWidth) TileWidth()
    //{
    //    var obj = Dino_Ground_Manager.instance.move;
    //    if (obj.groundSprite != null && obj.holeSprite != null)
    //    {


    //        return (obj.groundSprite.bounds.size.x, obj.holeSprite.bounds.size.x);
    //    }
    //    else
    //    {
    //        Debug.Log("groundSprite is null");
    //    }
    //    return (groundWidth, holeWidth);
    //}
}
