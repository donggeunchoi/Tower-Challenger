using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform objTarget;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 태그로 플레이어 구분
        {
            other.transform.position = objTarget.position;
            other.gameObject.layer = 21;
        }
    }
}
