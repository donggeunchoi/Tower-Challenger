using UnityEngine;
public enum DebuffType
{
    None,
    Slow,
    Stun,
    Hit
}
public class Trap : MonoBehaviour
{
    [SerializeField] private DebuffType type;
    public string trapId;
    private PlayerBuff playerBuff;

    private void Start()
    {
        int randomValue;
        if (StageTable.trapPerTableList[0] != null)
        {
            int randomCount = StageTable.trapPerTableList[0].hitPer + StageTable.trapPerTableList[0].slowPer + StageTable.trapPerTableList[0].stuPer;
            randomValue = Random.Range(1, randomCount + 1);

            if (randomValue <= StageTable.trapPerTableList[0].hitPer)
                type = DebuffType.Hit;
            else if (randomValue <= StageTable.trapPerTableList[0].hitPer + StageTable.trapPerTableList[0].slowPer)
                type = DebuffType.Slow;
            else if (randomValue <= randomCount)
                type = DebuffType.Stun;
            else
                type = DebuffType.None;
        }
        else
        {
            randomValue = Random.Range(1, 101);

            if (randomValue < 51)                           //임시 50%
                type = DebuffType.Slow;
            else if (randomValue > 50 && randomValue < 91)  //임시 40%
                type = DebuffType.Stun;
            else if (randomValue > 90 && randomValue < 96)  //임시 5%
                type = DebuffType.Hit;
            else                                            //임시 5%
                type = DebuffType.None;
        }
    }

    private void AddDebuff(GameObject player)
    {
        switch (type)
        {
            case DebuffType.Slow:
                player.TryGetComponent<PlayerBuff>(out playerBuff);
                if (playerBuff != null)
                    playerBuff.SlowDebuff();
                break;
            case DebuffType.Stun:
                player.TryGetComponent<PlayerBuff>(out playerBuff);
                if (playerBuff != null)
                    playerBuff.StunDebuff();
                break;
            case DebuffType.Hit:
                player.TryGetComponent<PlayerBuff>(out playerBuff);
                if (playerBuff != null)
                    playerBuff.isHit();

                if (StageManager.instance != null)
                    StageManager.instance.MiniGameResult(false);
                break;
            default:
                break;
        }
        DestroyTrapInfo();
        Destroy(this.gameObject);
    }

    public void DestroyTrapInfo()
    {
        MapInfo.StageTempMemory.destroyedInfo.destroyedTrapIds.Add(trapId);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AddDebuff(collision.gameObject);
        }
    }
}
