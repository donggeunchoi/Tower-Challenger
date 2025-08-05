using UnityEngine;
using System.Collections;
using System.Linq;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using Unity.VisualScripting;

public class PlayerBuff : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    [SerializeField] private float effectTime;
    private StageTable.DebuffTable slowDebuff;
    private StageTable.DebuffStunTableRow stunDebuff;
    [SerializeField] private float slowSpeed;

    public SpriteRenderer playerSprite;

    public bool isSlow;
    public bool isStun;


    [SerializeField] private int difficulty;

    private Coroutine slowCor;
    private Coroutine stunCor;

    private Coroutine slowEftCor;
    private Coroutine stunEftCor;
    private Coroutine hitEftCor;

    private void Start()
    {
        CVSLoader.LoadDebuffCSV();
        CVSLoader.LoadDebuffStunCSV();

        isSlow = false;
        isStun = false;
    }

    private void RestorePlayerColor()
    {
        if (playerSprite != null)
            playerSprite.color = new Color (1,1,1,1);
    }

    private IEnumerator SlowEftCor()
    {
        Color playerColor = playerSprite.color;
        float duration = 0.5f;

        while (isSlow)
        {
            float time = 0f;
            while (time < duration)
            {
                playerSprite.color = new Color(time / duration, playerColor.g, playerColor.b, playerColor.a);
                time += Time.deltaTime;
                yield return null;
            }

            time = 0f;

            while (time < duration)
            {
                playerSprite.color = new Color((duration - time) / duration, playerColor.g, playerColor.b, playerColor.a);
                time += Time.deltaTime;
                yield return null;
            }
        }
        playerSprite.color = playerColor;

        yield return null;
    }

    private IEnumerator HitEftCor()
    {
        bool isHit = true;
        int stack = 3;
        int currentStack = 0;
        Color playerColor = playerSprite.color;
        float duration = 0.2f;

        while (isHit)
        {
            float time = 0f;
            while (time < duration)
            {
                playerSprite.color = new Color(playerColor.r, playerColor.g, playerColor.b, 1/2);
                time += Time.deltaTime;
                yield return null;
            }

            time = 0f;

            while (time < duration)
            {
                playerSprite.color = new Color(playerColor.r, playerColor.g, playerColor.b, playerColor.a);
                time += Time.deltaTime;
                yield return null;
            }

            currentStack++;
            if (currentStack == stack)
            {
                isHit = false;
            }
        }
        playerSprite.color = playerColor;

        yield return null;
    }

    private IEnumerator StunEftCor()
    {
        Color playerColor = playerSprite.color;
        float duration = 0.5f;

        while (isStun)
        {
            float time = 0f;
            while (time < duration)
            {
                playerSprite.color = new Color(playerColor.r, playerColor.g, 0, playerColor.a);
                time += Time.deltaTime;
                yield return null;
            }

            time = 0f;

            while (time < duration)
            {
                playerSprite.color = new Color(playerColor.r, playerColor.g, playerColor.b, playerColor.a);
                time += Time.deltaTime;
                yield return null;
            }
        }

        playerSprite.color = playerColor;

        yield return null;
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

    public void isHit()
    {
        if (playerSprite != null)
        {
            if (hitEftCor != null)
            {
                RestorePlayerColor();
                StopCoroutine(hitEftCor);
            }
            hitEftCor = StartCoroutine(HitEftCor());
        }
    }

    public void SlowDebuff()
    {
        CheckFloor();

        if (slowDebuff == null || string.IsNullOrEmpty(slowDebuff.downSP)) return;

        input.speed = input.originSpeed;

        isSlow = true;

        if (slowCor != null)
            StopCoroutine(slowCor);
        if (slowEftCor != null)
            StopCoroutine(slowEftCor);

        slowCor = StartCoroutine(SlowEffect(input));

        if (playerSprite != null)
        {
            RestorePlayerColor();
            slowEftCor = StartCoroutine(SlowEftCor());
        }
    }

    private IEnumerator SlowEffect(PlayerInput playerInput)
    {
        float slowSpeed = ParsePercent(slowDebuff.downSP);
        float beforSpeed = playerInput.speed;
        float time = 0;

        while (time < slowDebuff.eftTime)
        {
            playerInput.speed = playerInput.originSpeed * (1f - (slowSpeed / 100f));
            time += Time.deltaTime;
            yield return null;
        }

        playerInput.speed = playerInput.originSpeed;

        isSlow = false;

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

        if (playerSprite != null)
        {
            if (stunEftCor != null)
            {
                RestorePlayerColor();
                StopCoroutine(stunEftCor);
            }
            stunEftCor = StartCoroutine(StunEftCor());
        }
    }

    private IEnumerator StunEffect(PlayerInput playerInput)
    {
        isStun = true;

        float beforSpeed = playerInput.speed;
        float time = 0;

        while (time < stunDebuff.eftTime)
        {
            playerInput.speed = 0;
            time += Time.deltaTime;
            yield return null;
        }

        playerInput.speed = playerInput.originSpeed;

        isStun = false;

        yield return null;
    }

    private float ParsePercent(string percent)
    {
        if (string.IsNullOrEmpty(percent)) return 0f;
        percent = percent.Replace("%", "");
        float.TryParse(percent, out var num);
        return num;
    }
}
