using UnityEngine;
using System.Collections;

public class MiniGamePotal : MonoBehaviour
{
    StageManager stageManager;

    private void Awake()
    {
        stageManager = StageManager.Instance;
    }

    private IEnumerator Start()
    {
        // 최대 10프레임 대기 (0.5초)
        int maxWait = 10;
        while (StageManager.Instance == null && maxWait > 0)
        {
            yield return new WaitForEndOfFrame();
            maxWait--;
        }

        if (StageManager.Instance != null)
        {
            StageManager.Instance.StartRandomMiniGame(this.gameObject);
        }
        else
        {
            Debug.LogError("StageManager 인스턴스를 찾을 수 없습니다!");
            Destroy(gameObject); // 포탈 제거
        }
    }
}
