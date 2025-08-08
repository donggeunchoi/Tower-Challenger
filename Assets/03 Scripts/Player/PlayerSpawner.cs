using System.Collections;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer playerRenderer;

    private void Start()
    {
        player = null;
        StartCoroutine(SpawnPlayerCor());
    }

    IEnumerator SpawnPlayerCor()
    {
        while (player == null)
        {
            if (PlayerManager.Instance != null)
                player =  PlayerManager.Instance.playerPrefab;
            yield return null;
        }

        if (player != null)
        {
            GameObject equipPlayer = Instantiate(player, this.transform.position, Quaternion.identity);
            equipPlayer.layer = 21;
            equipPlayer.TryGetComponent<SpriteRenderer>(out playerRenderer);
            if (playerRenderer != null)
                playerRenderer.sortingLayerName = "Layer 2";
        }
        yield return null;
    }
}
