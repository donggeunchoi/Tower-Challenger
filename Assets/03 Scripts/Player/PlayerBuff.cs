using UnityEngine;
using System.Collections;

public class PlayerBuff : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    [SerializeField] private float effectTime;

    [SerializeField] private float slowSpeed;

    private Coroutine slowCor;
    private Coroutine stunCor;

    public void SlowDebuff()
    {
        input.speed = input.originSpeed;
        if (slowCor != null)
            StopCoroutine(slowCor);
        slowCor = StartCoroutine(SlowEffect(input));
    }

    private IEnumerator SlowEffect(PlayerInput playerInput)
    {
        float beforSpeed = playerInput.speed;
        playerInput.speed = playerInput.speed - (playerInput.speed * (slowSpeed / 100));
        yield return new WaitForSeconds(effectTime);
        playerInput.speed = playerInput.originSpeed;
        yield return null;
    }

    public void StunDebuff()
    {
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
        yield return new WaitForSeconds(effectTime);
        playerInput.speed = playerInput.originSpeed;
        yield return null;
    }
}
