using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public GameObject player;
    public GameObject playerPrefab;

    public GameObject player;

    public Vector3 playerPosition;    //플레이어 포지션 저장
    public int layerNumber;           //플레이어 레이어 저장

    public bool isMove;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadPlayer(GameObject playerObject)
    {
        playerPrefab = playerObject;
    }

    public void SavePlayerPosition(Vector3 position, int layer)
    {
        layerNumber = layer;
        playerPosition = position;
    }
    public void AfterLoadData() //플레이어를 찾아 해당 포지션으로 이동 (오류나서 코루틴으로 변경)
    {
        StartCoroutine(TowerManager.Instance.AfterLoadData());
    }

    public void PlayerSetting()
    {
        player = null;
        player = Instantiate(playerPrefab);

        if (player != null)
        {
            player.layer = layerNumber;
            string LayerName;
            switch (layerNumber)
            {
                case 20:
                    LayerName = "Layer 1";
                    break;
                case 21:
                    LayerName = "Layer 2";
                    break;
                default:
                    LayerName = "Layer 3";
                    break;
            }
            player.GetComponent<SpriteRenderer>().sortingLayerName = LayerName;
            player.transform.position = playerPosition;   //플레이어 위치를 수정
        }
    }
}
