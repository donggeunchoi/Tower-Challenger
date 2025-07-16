using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public TutorialManager tutorialManager; // 연결 필요
    public GameObject tutorialPortal;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 플레이어 태그를 비교
        {
            tutorialManager.TriggerNextPanel();
            Destroy(gameObject); // 트리거 재사용 안 하려면 제거
        }
    }
    
}
