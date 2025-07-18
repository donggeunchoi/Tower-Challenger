using TMPro;
using UnityEngine;

public class StageInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI floorText;
    [SerializeField] private TextMeshProUGUI stageText;

    private StageManager stageManager;
    private UIManager uIManager;

    private void Start()
    {
        uIManager = UIManager.Instance;
        stageManager = StageManager.instance;
    }

    private void Update()
    {
        if (floorText != null && stageManager != null)
            floorText.text = "Floor : " + stageManager.floor;

        if (stageText != null && stageManager != null)
            stageText.text = "Stage Count : " + stageManager.stageClearPortal.Count;

        if (stageText != null && stageManager != null )
        {
            if (stageManager.floor % StageManager.BOSS_FLOOR == 0)
                stageText.text = "Boss";
        }
    }
}
