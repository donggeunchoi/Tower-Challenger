using UnityEngine;
using System.Collections;
using System.Linq;
using Unity.VisualScripting.AssemblyQualifiedNameParser;

public class PlayerBuff : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    [SerializeField] private float effectTime;
    private StageTable.DebuffTable slowDebuff;
    private StageTable.DebuffStunTableRow stunDebuff;
    [SerializeField] private float slowSpeed;

    [SerializeField] private int difficulty;

    private Coroutine slowCor;
    private Coroutine stunCor;

    private void Start()
    {
        CVSLoader.LoadDebuffCSV();
        CVSLoader.LoadDebuffStunCSV();
    }

    private void CheckFloor()
    {
        if (StageManager.instance != null)
        {
            int floor = StageManager.instance.floor;
            slowDebuff = StageTable.debuffSlowTableList.FirstOrDefault(x => x.floor == floor);
            stunDebuff = StageTable.debuffStunTableList.FirstOrDefault(x => x.floor == floor);
        }
        else
        {
            slowDebuff = StageTable.debuffSlowTableList.FirstOrDefault(x => x.floor == 1);
            stunDebuff = StageTable.debuffStunTableList.FirstOrDefault(x => x.floor == 1);
        }
    }

    public void SlowDebuff()
    {
        CheckFloor();

        if (slowDebuff == null || string.IsNullOrEmpty(slowDebuff.downSP)) return;

        input.speed = input.originSpeed;
        if (slowCor != null)
            StopCoroutine(slowCor);
        slowCor = StartCoroutine(SlowEffect(input));
    }

    private IEnumerator SlowEffect(PlayerInput playerInput)
    {
        float slowSpeed = ParsePercent(slowDebuff.downSP);

        float beforSpeed = playerInput.speed;
        playerInput.speed = playerInput.speed - (playerInput.speed * (slowSpeed / 100));
        yield return new WaitForSeconds(slowDebuff.eftTime);
        playerInput.speed = playerInput.originSpeed;
        yield return null;
    }

    public void StunDebuff()
    {
        CheckFloor();

        if (stunDebuff == null) return;

        input.speed = input.originSpeed;
        if (slowCor != null)
            StopCoroutine(slowCor);
        if (stunCor != null)
            StopCoroutine(stunCor); 
        stunCor = StartCoroutine(StunEffect(input));
    }

    private IEnumerator StunEffect(PlayerInput playerInput)
    {
        float beforSpeed = playerInput.speed;
        playerInput.speed = 0;
        yield return new WaitForSeconds(stunDebuff.eftTime);
        playerInput.speed = playerInput.originSpeed;
        yield return null;
    }

    private float ParsePercent(string percent)
    {
        if (string.IsNullOrEmpty(percent)) return 0;
        percent = percent.Replace("%", "");
        float.TryParse(percent, out var num);
        return num;
    }
}
