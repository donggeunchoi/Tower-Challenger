using UnityEngine;

public class MiniGameController : MonoBehaviour
{
    public void ReportSuccess() => StageManager.Instance.EndMiniGame(true);
}
