using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToTheVillage : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // 태그로 플레이어 구분
        {
            SceneManager.LoadScene("VillageScene");
        }
    }
}
