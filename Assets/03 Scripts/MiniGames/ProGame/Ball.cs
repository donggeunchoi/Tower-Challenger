using System.Collections;
using UnityEngine;


public class Ball : MonoBehaviour
{
    private float speed = 5f;

    // 매니저에서 속도 세팅 + 반환 타이머 시작
    public void SetSpeed(float moveSpeed)
    {
        speed = moveSpeed;

        // 중복 실행 방지
        StopAllCoroutines();
        StartCoroutine(ReturnToPoolAfterSeconds(10f));
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    // 일정 시간 후 풀에 반환
    private IEnumerator ReturnToPoolAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (PoolManager.Instance != null)
        {
            PoolManager.Instance.ReturnObject(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject); // 예외 처리
        }
    }
}

