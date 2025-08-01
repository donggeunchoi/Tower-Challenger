﻿using UnityEngine;
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
        int randomValue = Random.Range(0, 100);

        type = DebuffType.None;

        //if (randomValue < 51)                           //임시 50%
        //    type = DebuffType.Slow;
        //else if (randomValue > 50 && randomValue < 91)  //임시 40%
        //    type = DebuffType.Stun;
        //else if (randomValue > 90 && randomValue < 96)  //임시 5%
        //    type = DebuffType.Hit;
        //else                                            //임시 5%
        //    type = DebuffType.None;
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
        MapInfo.StageTempMemory.destroyedInfo.destroyedTrapIds.Add(trapId);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AddDebuff(collision.gameObject);
        }
    }
}
